using System;
using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    Weapon weapon;
    GameManager gameManager;
    ItemManager itemManager;
    Vector3 moveDir;
    private Transform mobTrnspos;
    private float beforeX;
    CapsuleCollider2D mobCollider;

    [Header("���� ����")]
    int HP = 1;//�ۺ� ��ȯ ���ɼ� ����
    Animator anim;
    float deathTime = 0.3f;
    float deathTimer = 0.2f;
    [SerializeField] float moveSpeed = 0.5f;//�� �̵� �ӵ�


    private void Awake()
    {
        mobTrnspos = transform;
        gameManager = FindObjectOfType<GameManager>();
        //����� ����
        //public ������ ������ ������ ����
        //�ڵ� �ϼ� ������� ����
        beforeX = mobTrnspos.position.x;//������ x�� Ȯ��
        mobCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            //HP = HP - 1;
            //death();

            deamageCheack();
        }
    }
    /// <summary>
    /// ������ ���
    /// </summary>
    private void deamageCheack()
    {
        int MaxHP = HP;
        //Debug.Log($"MaxHP = {MaxHP}");
        MaxHP = weapon.DamageFigure(MaxHP);//MaxHP - 5
        Debug.Log($"MaxHP = {MaxHP}");
        HP = MaxHP;
    }

   /// <summary>
   /// ������� üũ
   /// </summary>
    private void deathCheck()
    {
        if (HP <= 0)
        {
            anim.SetBool("Death", true);
            deathTimer += deathTimer * Time.deltaTime;//�ִϸ��̼� ����ð�
            //Destroy(gameObject);
            if (deathTimer >= deathTime)
            {
                Destroy(gameObject);
                deathTimer = 0.0f;
            }
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
            return;
        }
        else 
        {
            gameManager.PlayerTrsPosiTion(out playerPos);//��¿�
            Vector3 distance = playerPos - mobTrnspos.position;
            distance.z = 0.0f;
            mobTrnspos.Translate(distance.normalized * moveSpeed * Time.deltaTime);
            //Debug.Log($"{distance}");//�����ʿ�

        }

    }
    /// <summary>
    /// �̵��ϴ� ���⿡ ���� Scale�� ������ �¿�� �̵��ϴ°� ó�� ���̰� �Ѵ�
    /// </summary>
    private void seeCheack()
    {
        Vector3 scale = mobTrnspos.localScale;
        float affterX = mobTrnspos.position.x;
       
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
