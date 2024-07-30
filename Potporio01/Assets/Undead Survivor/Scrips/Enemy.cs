using System;
using System.Collections;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




public class Enemy : MonoBehaviour
{

    public enum MobTag
    {
       Skull,
       Defolt,
       White,

    }
    [SerializeField] MobTag Type;//�� Ÿ��

    GameManager gameManager;
    Transform MobTrnspos;
    float beforeX;
    PlayerStatas playerStatas;

    [Header("���� ����")]
    [SerializeField] int mobHP = 1;//���� ü��
    Animator anim;
    float deathTime = 0.3f;
    float deathTimer = 0f;
    [SerializeField] public float moveSpeed = 0.5f;//�� �̵� �ӵ�
    //public bool Mobnull = false;//�����ʿ�
    int MobDamage = 1;
    [SerializeField] Sprite hitImg;
    Color defolteSprit;
    SpriteRenderer spriteRenderer;
    
    [Header("���� ��ų")]
    float SkillCoolTimer = 0f;
    float SkillCoolTime = 1.0f;
    float SkillRunTimer = 0f;
    float SkillRunTime = 1.0f; 
    bool FatenOn = true;
    public bool statasUp = true;
    Vector3 FatenPos;
    [SerializeField] GameObject WhithWeapon;
    [SerializeField] bool CodeOn = true;
    [SerializeField] bool moving = false;
    bool skillChasing = false;
    float skillChasingTimer =0f;
    TrailRenderer skullTrailRenderer;

    [Header("�ı��� ����")]
    [SerializeField] int score;
    Vector3 trspos;
    CapsuleCollider2D Capcollider;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defolteSprit = spriteRenderer.color;
        Capcollider = GetComponent<CapsuleCollider2D>();
    }
    private void Start()
    {
        pluse();
        MobTrnspos = transform;
        beforeX = MobTrnspos.position.x;//������ x�� Ȯ��
        playerStatas = FindObjectOfType<PlayerStatas>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Sword"))
        {
           Weapon weapon = collision.gameObject.GetComponent<Weapon>();

            if (weapon != null) 
            {
                weapon.WeapondeamageCheack(out int damage);
                mobHP -= damage;
                deathCheck();
            }
            
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            mobHP -= 1;
            deathCheck();
        }
    }
    private void Update()
    {
        if (gameManager.objStop == true) { return; }
        if (playerStatas == null) { return; }
        mobPattern();
        seeCheack();
    }
    private void pluse() //������ ü�� Ȯ��
    {
        mobHP = mobHP + gameManager.PluseHp;
        moveSpeed = moveSpeed + gameManager.PluseHp;
    }
    private void mobPattern()
    {
        if (Type == MobTag.Defolt) 
        {
            Mobmoving(CodeOn);
        }
        else if (Type == MobTag.Skull)
        {
            skullTrailRenderer = GetComponent<TrailRenderer>();
            skullTrailRenderer.enabled = false;
            Mobmoving(CodeOn);
            Patern();
        }
        else if (Type == MobTag.White) 
        {
            Mobmoving(CodeOn);
            Patern();
        }
    }

    private void Patern()
    {
        gameManager.PlayerTrsPosiTion(out Vector3 playerPos);//��¿�
        Vector3 defolt = playerPos - transform.position;
        if (Vector3.Distance(playerPos , transform.position) < 1.5f)
        { 
            CodeOn = false;
            if (FatenOn == true)
            {
                FatenPos = playerPos;
                FatenOn = false;
            }
            if (Type == MobTag.Skull)
            {
                SkullSkill(FatenPos);
            }
            else
            {
                WhiteSkill();
            }
        }
        else
        {
            CodeOn = true;
            moving = true;
            FatenOn = true;
        }
    }

    private void SkullSkill(Vector3 defolt) 
    {
        if (moving == true)
        {
            skullTrailRenderer.enabled = true;
            Vector3 direction = defolt - transform.position;
            transform.position += (direction * 2 * Time.deltaTime);
            SkillRunTimer += Time.deltaTime;
            if (SkillRunTimer > SkillRunTime)
            {
                SkillRunTimer = 0f;
                moving = false;
                skullTrailRenderer.enabled = false;
            }
        }
        else 
        {
            SkillCoolTimer += Time.deltaTime;
            if (SkillCoolTimer > SkillCoolTime)
            {
                FatenOn = true;
                SkillCoolTimer = 0f;
                moving = true;
            }

        }
    }
    private void WhiteSkill()
    { 
        if (moving == true)
        {
            Vector3 trspos = transform.position;//�� ��ġ
            GameObject go = Instantiate(WhithWeapon, trspos,
                Quaternion.identity, gameManager.CreatTab);
            moving = false;
            
        }
        else 
        {
            SkillCoolTimer += Time.deltaTime;
            if (SkillCoolTimer > SkillCoolTime) 
            {
                moving = true;
                SkillCoolTimer = 0.0f;
            }
        }
        
    }

    /// <summary>
    /// �÷��̾ �ִ� �������� �ڵ����� �̵��մϴ�
    /// ������ �߰��ϸ� ��� ����(����! public��)
    /// </summary>
    private void Mobmoving(bool ON) 
    {
        if (ON == false || mobHP <= 0) { return; }
        gameManager.PlayerTrsPosiTion(out Vector3 playerPos);//��¿�
        Vector3 distance = playerPos - MobTrnspos.position;
        distance.z = 0.0f;
        MobTrnspos.Translate(distance.normalized * moveSpeed * Time.deltaTime);

        //Debug.Log(distance);
        //Debug.Log($"{distance}");//�����ʿ�

    }
    /// <summary>
    /// �̵��ϴ� ���⿡ ���� Scale�� ������ �¿�� �̵��ϴ°� ó�� ���̰� �Ѵ�
    /// </summary>
    private void seeCheack()
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
    private void deathCheck()
    {
        
        if (mobHP <= 0)
        {
            Capcollider.enabled = false;
            if (Type != MobTag.Defolt) //��ų �ߵ� ���߱�
            {
                Type = MobTag.Defolt;
            }
            anim.SetBool("Death", true);
            gameManager.mobSpowncount -= 1;
            gameManager.CreateItemProbability(transform.position);
            gameManager.ScorePluse(score);
            Invoke("destroy", 0.4f); 
        }
        else 
        {
            anim.SetTrigger("Hit");
        }

    }

    private void destroy() 
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// ���� ������
    /// </summary>
    /// <param name="_iNum"></param>
    public void MobDemageCheack(out int _iNum)//<-class Enemy
    {
        _iNum = MobDamage;

    }
}
