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

    [Header("���� ����")]
    [SerializeField] protected int HP;//�ۺ� ��ȯ ���ɼ� ����
    protected Animator anim;
    protected float deathTime = 0.3f;
    protected float deathTimer = 0f;
    [SerializeField] protected float moveSpeed = 0.5f;//�� �̵� �ӵ�
    public bool Mobnull = false;//�����ʿ�
    int MobDamage = 1;

    [Header("�ı��� ����")]
    [SerializeField] protected int score;


    protected void Awake()
    {
        MobTrnspos = transform;
        beforeX = MobTrnspos.position.x;//������ x�� Ȯ��
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
            deathTimer += Time.deltaTime;//�ִϸ��̼� ����ð�
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
    /// �÷��̾ �ִ� �������� �ڵ����� �̵��մϴ�
    /// ������ �߰��ϸ� ��� ����(����! public��)
    /// </summary>
    protected virtual void Mobmoving() 
    {
        if (playerStatas == null) { return; }
        Vector3 playerPos;
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
}
