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
    [Header("�÷��̾� ����")]
    [SerializeField, Range(1, 10)] int plLevel = 1;//����,ȭ�鿡 ǥ�õǰ� �ϰ� �ʹ�
    public float beforHp;
    public float NowHp = 5;//�ð������� ���̴� ���� ü��
    public float MaximumHP = 5;//�ִ� ü��
    [SerializeField] int score = 0;//����
    float deathTime = 1.0f;//���ó�� �ð�
    float deathTimer = 0.0f;//
    bool invicibility = false;
    float invicibilityTimer = 0;
    float invicibilityTime = 1f;
    SpriteRenderer spriteRenderer;
    [Header("�÷��̾� ����")]
    //public bool Alive = true;//���� ����
    public bool DropSword = false;//�ڵ� ��� �� ��� ����
    public bool DropOpGun = false;//�ڵ� ���� ��� ����
    public bool DropBoundGun = false;//�ڵ� ���� ��� ����
    public bool DropSwordScaleUP = false;//�ڵ� ���� ��� ����
    public bool DropSwordUgaid = false;//�ڵ� ���� ��� ����

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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet")) { return; }
        string name = "";
        Item item = collision.gameObject.GetComponent<Item>();

        if (collision.gameObject.layer == LayerMask.NameToLayer("Mob"))//�����ð� ���� ����
        {
            if (invicibility == true) { return; }
            enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                if ((NowHp > 0)) { OnInvicibility(true); }
                int damage;
                enemy.MobDemageCheack(out damage);
                NowHp -= damage;
                deathCheck();
                //GameManager.Instance.Hpcheck(NowHp, MaximumHP);//�ϴ� ���� ����
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
            item.GetItenType(out name);
            if (NowHp < MaximumHP)//��+
            {
                NowHp += 1;
            }
            gameManager.Tooltip(name);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Speed"))//���ǵ�UP
        {
            item.GetItenType(out name);
            moveControll = GetComponent<MoveControll>();
            if (moveControll.MaxiumSpeed > moveControll.moveSpeed)
            {
                moveControll.moveSpeed += 0.5f;
            }
            else { gameManager.ItemKind.RemoveAt(gameManager.itemKindNmber); }
            gameManager.Tooltip(name);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("OpGun"))//�ڵ� �Ѿ� �ر�
        {
            item.GetItenType(out name);
            if (DropOpGun == false)
            {
                gameManager.Tooltip(name);
                DropOpGun = true;
                gameManager.WeaponKind.RemoveAt(gameManager.WeaponKindNmber);
            }
            else { gameManager.Tooltip(name); }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("BoundGun"))//�ٿ �Ѿ� �ر�
        {
            item.GetItenType(out name);
            if (DropBoundGun == false)
            {
                gameManager.Tooltip(name);
                DropBoundGun = true;
                gameManager.WeaponKind.RemoveAt(gameManager.WeaponKindNmber);
            }
            else { gameManager.Tooltip(name); }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("UnlockSword"))//�� �ر�
        {
            item.GetItenType(out name);
            if (DropSword == false) 
            { 
                gameManager.Tooltip(name);
                DropSword = true;
                gameManager.WeaponKind.RemoveAt(gameManager.WeaponKindNmber);
            }
            else { gameManager.Tooltip(name); }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("MaxHpUp"))//�ִ�ü��Up
        {
            item.GetItenType(out name);
            MaximumHP += 1;
            gameManager.Tooltip(name);
            Destroy(collision.gameObject);
        }

    }
    public float GetHP(out float now) 
    {
        now = NowHp;
        return now;
    }
    public float GetMaxHP(out float max) 
    {
        max = MaximumHP;
        return max;
    }

    private void Update()
    {
        checkinvicibility();
    }

    private void checkinvicibility() //üũ��
    {
        if (invicibility == false)  return; 
        invicibilityTimer += Time.deltaTime;
        if (invicibilityTimer > invicibilityTime)
        {
            OnInvicibility(false);
        }
    }
    private void OnInvicibility(bool value)//���� Ÿ��
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
    /// ����� ����
    /// </summary>
    private void deathCheck() //Ʈ���� �ʿ�
    {
        if (NowHp <= 0)
        {
            NowHp = 0;
            invicibility = false;
            OnInvicibility(false);
            anim = GetComponent<Animator>();
            anim.SetBool("Death", true);
            Invoke("destroy", 1f);
        }
        
    }
    private void destroy()
    {
        Destroy(gameObject);
        gameManager.deadOn =true;     
    }
}
