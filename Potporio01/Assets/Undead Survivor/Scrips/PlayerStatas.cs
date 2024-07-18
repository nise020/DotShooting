using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatas : MonoBehaviour
{
    Enemy enemy;
    Animator anim;
    MoveControll moveControll;
    GameManager gameManager;
    CapsuleCollider2D capsuleCollider2D;
    [Header("플레이어 정보")]
    [SerializeField, Range(1, 10)] int plLevel = 1;//레벨,화면에 표시되게 하고 싶다
    public float beforHp;
    public float NowHp = 5;//시각적으로 보이는 현재 체력
    public float MaximumHP = 5;//최대 체력
    [SerializeField] int score = 0;//점수
    float deathTime = 1.0f;//사망처리 시간
    float deathTimer = 0.0f;//
    bool invicibility = false;
    float invicibilityTimer = 0;
    float invicibilityTime = 1f;
    SpriteRenderer spriteRenderer;
    [Header("플레이어 정보")]
    //public bool Alive = true;//생존 여부
    public bool DropSword = false;//자동 사냥 검 드랍 여부
    public bool DropOpGun = false;//자동 조준 드랍 여부
    public bool DropSwordScaleUP = false;//자동 조준 드랍 여부
    public bool DropSwordUgaid = false;//자동 조준 드랍 여부

    private void Awake()
    {
        NowHp = MaximumHP;
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
        spriteRenderer =GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Mob"))//무적시간 구현 예정
        {
            if (invicibility == true) { return; }
            enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                if ((NowHp > 0)) { OnInvicibility(true); }
                int damage = 0;
                enemy.MobDemageCheack(out damage);
                NowHp -= damage;
                deathCheck();
                //GameManager.Instance.Hpcheck(NowHp, MaximumHP);//일단 삭제 ㄴㄴ
                //Debug.Log($"NowHp={NowHp}");
            }

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("MobBullet")) 
        {
            if (invicibility == true) { return; }
            if ((NowHp > 0)) { OnInvicibility(true); }
            NowHp -= 1;
            deathCheck();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Heal"))//item
        {
            if (NowHp < MaximumHP)//힐+
            {
                NowHp += 1;
                //GameManager.Instance.Hpcheck(NowHp, MaximumHP);
                //Debug.Log($"NowHp={NowHp}");
            }
            else { return; }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Speed"))//스피드UP
        {
            moveControll = GetComponent<MoveControll>();
            if (moveControll.MaxiumSpeed > moveControll.moveSpeed)
            {
                moveControll.moveSpeed += 0.5f;
            }
            else { return; }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("OpGun"))//자동 총알 해금
        {
            DropOpGun = true;

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Sword"))//검 해금
        {
            DropSword = true;

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("MaxHpUp"))//최대체력Up
        {
            MaximumHP += 1;
        }

    }


    private void Update()
    {
        checkinvicibility();
    }

    private void checkinvicibility() //체크용
    {
        if (invicibility == false)  return; 
        invicibilityTimer += Time.deltaTime;
        if (invicibilityTimer > invicibilityTime)
        {
            OnInvicibility(false);
        }
    }
    private void OnInvicibility(bool value)//무적 타임
    {
        Color color = spriteRenderer.color;
        if (value == true)
        {
            color.a = 0.5f;
            invicibility = true;
            capsuleCollider2D.enabled = false;
            invicibilityTimer = 0;
        }
        else 
        {
            color.a = 1.0f;
            invicibility = false;
            capsuleCollider2D.enabled = true;
            invicibilityTimer = invicibilityTime;
        }
        spriteRenderer.color = color;
    }
    /// <summary>
    /// 사망시 연출
    /// </summary>
    private void deathCheck() //트리거 필요
    {
        if (NowHp <= 0)
        {
            NowHp = 0;
            anim = GetComponent<Animator>();
            anim.SetBool("Death", true);
            Invoke("destroy", 1f);
            //gameManager.rankCheck();//아직 시동 ㄴㄴ
            //deathTimer += Time.deltaTime;
            //Debug.Log(deathTimer);
            //if (deathTimer > deathTime)
            //{
            //    Alive = false;
               
                
            //}
        }
        
    }
    private void destroy()
    {
        Destroy(gameObject);
        gameManager.rankCheck();     
    }
}
