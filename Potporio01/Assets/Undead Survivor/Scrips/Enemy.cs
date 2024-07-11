using System;
using System.Collections;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;


public class Enemy : MonoBehaviour
{

    public enum MobTag
    {
       Skull,
       Defolt,
       White,

    }
    [SerializeField] MobTag Type;//�� Ÿ��

    Weapon weapon;
    OppositionBullet Opbullet;
    ItemManager itemManager;
    PlayerStatas playerStatas;
    Vector3 moveDir;
    CapsuleCollider2D mobCollider;
    GameManager gameManager;
    protected Transform MobTrnspos;
    protected float beforeX;

    [Header("���� ����")]
    [SerializeField] public int HP;//�ۺ� ��ȯ ���ɼ� ����
    protected Animator anim;
    protected float deathTime = 0.4f;
    protected float deathTimer = 0f;
    [SerializeField] protected float moveSpeed = 0.5f;//�� �̵� �ӵ�
    //public bool Mobnull = false;//�����ʿ�
    int MobDamage = 1;
    float SkillCoolTimer = 0f;
    float SkillCoolTime = 1f;
    float SkillRunTimer = 0f;
    float SkillRunTime = 1f; 
    bool FatenOn = true;
    Vector3 FatenPos;
    [SerializeField] GameObject WhithWeapon;
    [SerializeField] Transform craetTab;
    [SerializeField] bool CodeOn = true;
    bool moving = false;
    Vector3 tarketBe;
    [Header("�ı��� ����")]
    [SerializeField] int score;
    

    protected void Awake()
    {
        MobTrnspos = transform;
        beforeX = MobTrnspos.position.x;//������ x�� Ȯ��
        mobCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }
    protected void Start()
    {
        gameManager = GameManager.Instance;
        playerStatas = FindObjectOfType<PlayerStatas>();
        tarketBe = playerStatas.transform.position;
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
            HP -= 1;         
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            moving = false;
        }
    }
    protected void Update()
    {
        hpCheck();
        deathCheck();
        mobPattern();
        seeCheack();
    }
    private void hpCheck() 
    {
        if (gameManager.MobHPUp == false) { return; }
        if (gameManager.MobHPUp==true) 
        {
            HP += 1;
            gameManager.MobHPUp = false;
        }
    }
    private void mobPattern()
    {
        if (playerStatas == null) {  return; }
        if (Type == MobTag.Defolt) 
        {
            Mobmoving(CodeOn);
        }
        else if (Type == MobTag.Skull)
        {
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
        Vector3 targetpos = playerStatas.transform.position;// position;
        Vector3 defolt = targetpos - transform.position;
        if (defolt.x > -2.0f && defolt.y < 2.0f &&
            defolt.x < 2.0f && defolt.y > -2.0f)
        { 

            CodeOn = false;
            SkillCoolTimer += Time.deltaTime;
            if (SkillCoolTimer > SkillCoolTime)
            {
                if (FatenOn == true)
                {
                    FatenPos = targetpos;
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
        }
        else
        {
            SkillRunTimer = 0;
            SkillCoolTimer = 0;
            CodeOn = true;
            moving = true;
            FatenOn = true;
        }
    }

    private void WhiteSkill()
    {
        if (moving==true) 
        {
            Vector3 trspos = transform.position;//�� ��ġ
            GameObject go = Instantiate(WhithWeapon, trspos, 
                Quaternion.identity, gameManager.CreatTab);
            moving = false;
        }
        
    }

    private void SkullSkill(Vector3 defolt) 
    {
        Vector3 playerPos;//�÷��̾� ��ġ ������
        gameManager.PlayerTrsPosiTion(out playerPos);//��¿�
        Vector3 myScale = transform.lossyScale;
        Vector3 movingTrs = playerPos - transform.position;//�Ÿ�
        Vector3 Defolt = new Vector3(0, 0, 0);


        Vector3 PlMa = new Vector3(2, -2, 0);
        Vector3 MaPl = new Vector3(-2, 2, 0);
        Vector3 PlPl = new Vector3(2, 2, 0);
        Vector3 MaMa = new Vector3(-2, -2, 0);

        //Vector3 targetPosition = tarketBe;
        if (moving == true) 
        {
            if (myScale.x == 1)
            {
                if (movingTrs.x >= 0.0f && movingTrs.y >= 0.0f) //++
                {
                    Defolt = (PlPl + playerPos).normalized;
                }
                else if (movingTrs.x < 0.0f && movingTrs.y < 0.0f)//--
                {
                    Defolt = (MaMa + playerPos).normalized;
                }
                else if (movingTrs.x > 0.0f && movingTrs.y < 0.0f)//+-
                {
                    Defolt = (MaPl + playerPos).normalized;
                }
                else if (movingTrs.x < 0.0f && movingTrs.y > 0.0f)//-+
                {
                    Defolt = (PlMa + playerPos).normalized;
                }

            }
            else if (myScale.x == -1)
            {
                if (movingTrs.x > 0.0f && movingTrs.y > 0.0f) //++
                {
                    Defolt = (MaMa + playerPos).normalized;
                }
                else if (movingTrs.x <= 0.0f && movingTrs.y <= 0.0f)//--
                {
                    Defolt = (PlPl + playerPos).normalized;
                }
                else if (movingTrs.x > 0.0f && movingTrs.y < 0.0f)//+-
                {
                    Defolt = (PlMa + playerPos).normalized;
                }
                else if (movingTrs.x < 0.0f && movingTrs.y > 0.0f)//-+
                {
                    Defolt = (MaPl + playerPos).normalized;
                }
            }//����


            SkillRunTimer += Time.deltaTime;
            Vector3 direction = defolt - transform.position;
            transform.position += (direction * 2 * Time.deltaTime);
            if (SkillRunTimer> SkillRunTime) 
            {
                moving = false;
            }
           
        }
    }
    

    /// <summary>
    /// �÷��̾ �ִ� �������� �ڵ����� �̵��մϴ�
    /// ������ �߰��ϸ� ��� ����(����! public��)
    /// </summary>
    protected virtual void Mobmoving(bool ON) 
    {
        if (playerStatas == null || ON == false) { return; }
        Vector3 playerPos;//�÷��̾� ��ġ ������
        gameManager.PlayerTrsPosiTion(out playerPos);//��¿�
        Vector3 distance = playerPos - MobTrnspos.position;
        distance.z = 0.0f;
        MobTrnspos.Translate(distance.normalized * moveSpeed * Time.deltaTime);

        //Debug.Log(distance);
        //Debug.Log($"{distance}");//�����ʿ�

    }
    /// <summary>
    /// �̵��ϴ� ���⿡ ���� Scale�� ������ �¿�� �̵��ϴ°� ó�� ���̰� �Ѵ�
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
    protected virtual void deathCheck()
    {
        if (HP <= 0)
        {
            //Debug.Log(HP);
            anim.SetBool("Death", true);
            deathTimer += Time.deltaTime;//�ִϸ��̼� ����ð�
            //Debug.Log($"deathTimer={deathTimer}");
            if ((deathTimer > deathTime))
            {
                gameManager.CreateItemCheck(transform.position);
                Destroy(gameObject);
                gameManager.ScorePluse(score);
                deathTimer = 0f;
            }
   
        }
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
