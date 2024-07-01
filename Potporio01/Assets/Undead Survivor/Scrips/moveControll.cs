using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class moveControll : MonoBehaviour
{
    /// <summary>
    /// �÷��̾� �̵��ӵ�(���� �ο� ����)
    /// </summary>
    [SerializeField] public static float moveSpeed = 2.0f;
    Vector3 moveDir;
    Animator anim;
    CapsuleCollider2D cap2d;
    Rigidbody2D rigid;
    Vector2 moveVe2;
    Transform trsPos;

    //[Header("�ڵ� ���� Scale")]
    //[SerializeField] Transform autoHandScale;
    //[SerializeField] Transform autoFabScale;//���� ���� ����
    // Start is called before the first frame update
    private void Awake()
    {
        if (trsPos == null)//���� ����
        {
            trsPos = GetComponent<Transform>();
            Vector3 trsPosStart = new Vector3(0,0,0);
            transform.position = trsPosStart;
        }
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();
        cap2d = GetComponent<CapsuleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���� �ݶ��̴� ������ ���
    }
    // Update is called once per frame
    void Update()
    {
        playerMoving();
        runAnim();
        seeCheack();
    }
    /// <summary>
    /// ĳ���Ͱ� �̵��ϴ� ���⿡ ���� Scale��ġ�� ������ �¿츦 �����ϴ� ���
    /// </summary>
    private void seeCheack()
    {
        Vector3 scale1 = transform.localScale;
        if (Input.GetKeyDown(KeyCode.A) &&
            scale1.x != -1.0f && scale1.x == 1.0f)//��
        {
            scale1.x = -Mathf.Abs(scale1.x);
        }
        else if ((Input.GetKeyDown(KeyCode.D)) && 
            scale1.x != 1.0f && scale1.x == -1.0f)//��
        {
            scale1.x = Mathf.Abs(scale1.x);
        }
        transform.localScale = scale1;

    }

    /// <summary>
    /// ����Ű �Է½� �����̴� ���
    /// </summary>
    private void playerMoving()//������ ���� ����
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");// -1 0 1
        moveDir.y = Input.GetAxisRaw("Vertical");// -1 0 1

        transform.position += moveDir * moveSpeed * Time.deltaTime;
        //�����
        //moveDir.y = rigid.velocity.y;
        //rigid.velocity = moveDir;
        //MovePosition()

    }

    /// <summary>
    /// bool ������ ����ؼ� �����¿�� �Է½� �ִϸ��̼� ����
    /// </summary>
    private void runAnim() 
    {
        if (moveDir.x != 0)
        {
            anim.SetBool("Horizontal", true);
        }
        else
        {
            anim.SetBool("Horizontal", false);
        }
        if (moveDir.y != 0)
        {
            anim.SetBool("Vertical", true);
        }
        else
        {
            anim.SetBool("Vertical", false);
        }
    }
   
}
