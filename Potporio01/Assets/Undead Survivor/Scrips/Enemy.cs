using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static TagorDataList;


public class Enemy : MonoBehaviour
{
    Weapon weapon;
    OppositionBullet bullet;
    ItemManager itemManager;
    PlayerStatas playerStatas;
    Vector3 moveDir;
    CapsuleCollider2D mobCollider;
    protected GameManager gameManager;
    protected Transform MobTrnspos;
    protected float beforeX;

    [Header("몬스터 정보")]
    [SerializeField] protected int HP;//퍼블릭 전환 가능성 있음
    protected Animator anim;
    protected float deathTime = 0.3f;
    protected float deathTimer = 0f;
    [SerializeField] protected float moveSpeed = 0.5f;//몹 이동 속도
    public bool Mobnull = false;//수정필요
    int MobDamage = 1;

    [Header("파괴시 점수")]
    [SerializeField] protected int score;


    protected void Awake()
    {
        MobTrnspos = transform;
        beforeX = MobTrnspos.position.x;//기존에 x값 확인
        mobCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }
    protected void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerStatas = FindObjectOfType<PlayerStatas>();
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Sword"))
        {
            weapon = collision.gameObject.GetComponent<Weapon>();

            if (weapon != null) 
            {
                int damage = 0;
                weapon.WeapondeamageCheack(out damage);
                HP -= damage;
            }
            //deathCheck();
  
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            bullet = collision.gameObject.GetComponent<OppositionBullet>();
            
            if (bullet != null) 
            {
                int damage = 0;
                bullet.BulletdeamageCheack(out damage);
                HP -= damage;
                //Debug.Log(HP);
            }
            //deathCheck();

        }
    }
    protected void Update()
    {
        Mobmoving();
        seeCheack();
        deathCheck();
    }
    protected virtual void deathCheck()
    {
        if (HP <= 0)
        {
            //Debug.Log(HP);
            anim.SetBool("Death", true);
            deathTimer += Time.deltaTime;//애니메이션 재생시간
            //Debug.Log($"deathTimer={deathTimer}");
            if ((deathTimer > deathTime))
            {
                gameManager.CreateItemCheck(transform.position);
                Destroy(gameObject);
                deathTimer = 0f;
            }
   
        }
    }
    public void MobDemageCheack(out int _iNum)//<-class Enemy
    {
        _iNum = MobDamage;

    }

    /// <summary>
    /// 플레이어가 있는 방향으로 자동으로 이동합니다
    /// 보스를 추가하면 사용 예정(주의! public임)
    /// </summary>
    protected virtual void Mobmoving() 
    {
        if (playerStatas == null) { return; }
        Vector3 playerPos;
        gameManager.PlayerTrsPosiTion(out playerPos);//출력용
        Vector3 distance = playerPos - MobTrnspos.position;
        distance.z = 0.0f;
        MobTrnspos.Translate(distance.normalized * moveSpeed * Time.deltaTime);

        //Debug.Log(distance);
        //Debug.Log($"{distance}");//수정필요

    }
    /// <summary>
    /// 이동하는 방향에 따라서 Scale을 조절해 좌우로 이동하는것 처럼 보이게 한다
    /// </summary>
    protected void seeCheack()
    {
        Vector3 scale = MobTrnspos.localScale;
        float affterX = MobTrnspos.position.x;
       
        if (affterX > beforeX)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else if (affterX < beforeX)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
        beforeX = affterX;

    }
}
