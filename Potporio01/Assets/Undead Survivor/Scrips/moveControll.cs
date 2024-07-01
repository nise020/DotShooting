using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class moveControll : MonoBehaviour
{
    /// <summary>
    /// 플레이어 이동속도(버프 부여 가능)
    /// </summary>
    [SerializeField] public static float moveSpeed = 2.0f;
    Vector3 moveDir;
    Animator anim;
    CapsuleCollider2D cap2d;
    Rigidbody2D rigid;
    Vector2 moveVe2;
    Transform trsPos;

    //[Header("자동 공격 Scale")]
    //[SerializeField] Transform autoHandScale;
    //[SerializeField] Transform autoFabScale;//오토 공격 상태
    // Start is called before the first frame update
    private void Awake()
    {
        if (trsPos == null)//시작 지점
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
        //몬스터 콜라이다 닿으면 기능
    }
    // Update is called once per frame
    void Update()
    {
        playerMoving();
        runAnim();
        seeCheack();
    }
    /// <summary>
    /// 캐릭터가 이동하는 방향에 따라 Scale수치를 조절해 좌우를 보게하는 기능
    /// </summary>
    private void seeCheack()
    {
        Vector3 scale1 = transform.localScale;
        if (Input.GetKeyDown(KeyCode.A) &&
            scale1.x != -1.0f && scale1.x == 1.0f)//왼
        {
            scale1.x = -Mathf.Abs(scale1.x);
        }
        else if ((Input.GetKeyDown(KeyCode.D)) && 
            scale1.x != 1.0f && scale1.x == -1.0f)//오
        {
            scale1.x = Mathf.Abs(scale1.x);
        }
        transform.localScale = scale1;

    }

    /// <summary>
    /// 방향키 입력시 움직이는 기능
    /// </summary>
    private void playerMoving()//움직임 조절 가능
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");// -1 0 1
        moveDir.y = Input.GetAxisRaw("Vertical");// -1 0 1

        transform.position += moveDir * moveSpeed * Time.deltaTime;
        //참고용
        //moveDir.y = rigid.velocity.y;
        //rigid.velocity = moveDir;
        //MovePosition()

    }

    /// <summary>
    /// bool 형식을 사용해서 상하좌우로 입력시 애니메이션 실행
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
