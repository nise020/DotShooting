using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveControll : MonoBehaviour
{
    PlayerStatas playerStatas;
    GameManager gameManager;
    /// <summary>
    /// 플레이어 이동속도(버프 부여 가능)
    /// </summary>
    [SerializeField] public float moveSpeed = 2.0f;
    public float MaxiumSpeed = 6.0f;
    Vector2 moveDir;
    Animator moveAnim;
    Rigidbody2D rigid;
    Transform trsPos;

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
        moveAnim = transform.GetComponent<Animator>();
        gameManager = GameManager.Instance;
        playerStatas = GetComponent<PlayerStatas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.objStop == true) { return; }
        playerMoving();
        runAnim();
        seeCheack(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Line"))//크기 증가
        {
            Debug.Log(111);
        }
    }

    /// <summary>
    /// 캐릭터가 이동하는 방향에 따라 Scale수치를 조절해 좌우를 보게하는 기능
    /// </summary>
    private void seeCheack()
    {
        if (playerStatas.NowHp <= 0) { return; }
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
        if (playerStatas.NowHp <= 0) { return; }
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;// -1 0 1
        moveDir.y = Input.GetAxisRaw("Vertical") * moveSpeed;// -1 0 1

        rigid.velocity = moveDir;


    }

    /// <summary>
    /// bool 형식을 사용해서 상하좌우로 입력시 애니메이션 실행
    /// </summary>
    private void runAnim() 
    {
        
        if (moveDir.x != 0)
        {
            moveAnim.SetBool("Horizontal", true);
        }
        else
        {
            moveAnim.SetBool("Horizontal", false);
        }
        if (moveDir.y != 0)
        {
            moveAnim.SetBool("Vertical", true);
        }
        else
        {
            moveAnim.SetBool("Vertical", false);
        }
    }
   
}
