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

    [Header("�÷��̾� ����")]
    [SerializeField, Range(1, 10)] int plLevel = 1;//����,ȭ�鿡 ǥ�õǰ� �ϰ� �ʹ�
    public float NowHp = 5;//�ð������� ���̴� ���� ü��
    public float MaximumHP = 5;//�ִ� ü��
    int beforHP;
    [SerializeField] int score = 0;//����
    float deathTime = 1.0f;//���ó�� �ð�
    float deathTimer = 0.0f;//
    bool invicibility = false;
    float invicibilityTimer = 0;
    float invicibilityTime = 1;
    SpriteRenderer spriteRenderer;
    [Header("�÷��̾� ����")]
    public bool Alive = true;//���� ����
    public bool DropSword = false;//�ڵ� ��� �� ��� ����
    public bool DropOpGun = false;//�ڵ� ���� ��� ����
    public bool DropSwordScaleUP = false;//�ڵ� ���� ��� ����
    public bool DropSwordUgaid = false;//�ڵ� ���� ��� ����

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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Mob"))//�����ð� ���� ����
        {
            if (invicibility == true) { return; }
            enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                OnInvicibility(true);
                int damage = 0;
                enemy.MobDemageCheack(out damage);
                NowHp -= damage;
                //GameManager.Instance.Hpcheck(NowHp, MaximumHP);//�ϴ� ���� ����
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
            if (NowHp < MaximumHP)//��
            {
                NowHp += 1;
                //GameManager.Instance.Hpcheck(NowHp, MaximumHP);
                Debug.Log($"NowHp={NowHp}");
            }
            else { return; }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Speed"))//���ǵ�
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
    /// ����� ����
    /// </summary>
    private void deathCheck() //Ʈ���� �ʿ�
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
                //gameManager.rankCheck();//���� �õ� ����
            }
        }
        
    }
}
