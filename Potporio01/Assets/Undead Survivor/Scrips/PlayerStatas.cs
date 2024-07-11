using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatas : MonoBehaviour
{
    Enemy enemy;
    HpCanvers hpCanvers;
    Animator anim;
    MoveControll moveControll;
    GameManager gameManager;
    Weapon weapon;

    [Header("플레이어 정보")]
    [SerializeField, Range(1, 10)] int plLevel = 1;//레벨,화면에 표시되게 하고 싶다
    public float NowHp = 5;//시각적으로 보이는 현재 체력
    public float MaximumHP = 5;//최대 체력
    int beforHP;
    [SerializeField] int score = 0;//점수
    float deathTime = 1.0f;//사망처리 시간
    float deathTimer = 0.0f;//
    bool invicibility = false;
    float invicibilityTimer = 0;
    float invicibilityTime = 1;
    SpriteRenderer spriteRenderer;
    [Header("플레이어 정보")]
    public bool Alive = true;//생존 여부
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
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameManager.Instance;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Mob"))//무적시간 구현 예정
        {
            if (invicibility == true) { return; }
            enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                OnInvicibility(true);
                int damage = 0;
                enemy.MobDemageCheack(out damage);
                NowHp -= damage;
                //GameManager.Instance.Hpcheck(NowHp, MaximumHP);//일단 삭제 ㄴㄴ
                Debug.Log($"NowHp={NowHp}");
            }

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("MobBullet")) 
        {
            if (invicibility == true) { return; }
            OnInvicibility(true);
            NowHp -= 1;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Heal"))//item
        {
            if (NowHp < MaximumHP)//힐
            {
                NowHp += 1;
                //GameManager.Instance.Hpcheck(NowHp, MaximumHP);
                Debug.Log($"NowHp={NowHp}");
            }
            else { return; }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Speed"))//스피드
        {
            moveControll = GetComponent<MoveControll>();
            if (moveControll.MaxiumSpeed > moveControll.moveSpeed)
            {
                moveControll.moveSpeed += 0.5f;
            }
            else { return; }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("OpGun"))//
        {
            DropOpGun = true;

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Sword"))//
        {
            DropSword = true;

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("MaxHpUp"))//
        {
            MaximumHP += 1;

        }

    }


    private void Update()
    {
        deathCheck();
        checkinvicibility();
    }

    private void checkinvicibility() 
    {
        if (invicibility == false)  return; 
        invicibilityTimer += Time.deltaTime;
        if (invicibilityTimer > invicibilityTime)
        {
            OnInvicibility(false);
        }
    }
    private void OnInvicibility(bool value) 
    {
        Color color = spriteRenderer.color;
        if (value == true)
        {
            color.a = 0.5f;
            invicibility = true;
            invicibilityTimer = 0;
        }
        else 
        {
            color.a = 1.0f;
            invicibility = false;
            invicibilityTimer = invicibilityTime;
        }
        spriteRenderer.color = color;
    }
    /// <summary>
    /// 사망시 연출
    /// </summary>
    private void deathCheck() //트리거 필요
    {
        if (NowHp <= 0 && Alive == true)
        {
            NowHp = 0;
            anim = GetComponent<Animator>();
            anim.SetBool("Death", true);
            deathTimer += Time.deltaTime;
            Debug.Log(deathTimer);
            if (deathTimer > deathTime)
            {
                Alive = false;
                Destroy(gameObject);
                //gameManager.rankCheck();//아직 시동 ㄴㄴ
            }
        }
        
    }
}
