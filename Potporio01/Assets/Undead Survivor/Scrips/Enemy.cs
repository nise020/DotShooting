using System;
using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    Weapon weapon;
    GameManager gameManager;
    ItemManager itemManager;
    Vector3 moveDir;
    Transform MobTrnspos;
    private float beforeX;
    CapsuleCollider2D mobCollider;

    [Header("���� ����")]
    public int HP = 1;//�ۺ� ��ȯ ���ɼ� ����
    Animator anim;
    float deathTime = 0.3f;
    float deathTimer = 0.2f;
    [SerializeField] float moveSpeed = 0.5f;//�� �̵� �ӵ�
    public bool Mobnull = false; 


    private void Awake()
    {
        MobTrnspos = transform;
        gameManager = FindObjectOfType<GameManager>();
        //����� ����
        //public ������ ������ ������ ����
        //�ڵ� �ϼ� ������� ����
        beforeX = MobTrnspos.position.x;//������ x�� Ȯ��
        mobCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            HP = HP - 1;
            deathCheck();

            //deamageCheack();
        }
    }
    /// <summary>
    /// ������ ���
    /// </summary>
    private void deamageCheack()//<-class Enemy
    {
        //int MaxHP = HP;//1
        HP = HP - weapon.WeaponDamage;//class.public int = 1
        return;
        //HP = MaxHP;
        deathCheck();//if (HP <= 0)
        //�� �κ��� �� �۵� �մϴ�
    }

   /// <summary>
   /// ������� üũ
   /// </summary>
    private void deathCheck()
    {
        if (HP <= 0)
        {
            anim.SetBool("Death", true);
            deathTimer += Time.deltaTime;//�ִϸ��̼� ����ð�
            //Destroy(gameObject);
            if (deathTimer >= deathTime)
            {
                Destroy(gameObject);
                Mobnull = true;
                deathTimer = 0.0f;
                gameManager.CreateItemCheck(transform.position);
            }
            Mobnull = false ;//��ġ ���� �ʿ�
        }
        
    }

    private void Update()
    {
        Mobmoving();
        seeCheack();
        deathCheck();
    }




    private void Atteak()//��⿹��
    {
        //�ӽñ��
        if (mobCollider.IsTouchingLayers(LayerMask.GetMask("Player")) == true) 
        {
            Destroy(gameObject);
        }
    }



    /// <summary>
    /// �÷��̾ �ִ� �������� �ڵ����� �̵��մϴ�
    /// ������ �߰��ϸ� ��� ����(����! public��)
    /// </summary>
    public void Mobmoving() 
    {
        Vector3 playerPos;
       
        if (gameManager.trsTarget == null)//player�� �׾��� ���
        {
            MobTrnspos = transform;
        }
        else 
        {
            gameManager.PlayerTrsPosiTion(out playerPos);//��¿�
            Vector3 distance = playerPos - MobTrnspos.position;
            distance.z = 0.0f;
            MobTrnspos.Translate(distance.normalized * moveSpeed * Time.deltaTime);
            //Debug.Log($"{distance}");//�����ʿ�

        }

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
}
