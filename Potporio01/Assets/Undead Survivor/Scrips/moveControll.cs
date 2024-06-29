using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveControll : MonoBehaviour
{

    [SerializeField] float moveSpeed = 2.0f;
    Vector3 moveDir;
    Animator anim;
    CapsuleCollider2D playerCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoving();
        RunAnim();
        seeCheack();
    }
    /// <summary>
    /// 캐릭터가 이동하는 방향에 따라 Scale수치를 조절해 좌우를 보게하는 기능
    /// </summary>
    private void seeCheack()
    {
        Vector3 scale = transform.localScale;
        if (Input.GetKeyDown(KeyCode.A) && scale.x != -1.0f)//-
        {
            scale.x = -Mathf.Abs(scale.x);
            //Debug.Log("왼");
        }
        else if ((Input.GetKeyDown(KeyCode.D)) && scale.x != 1.0f)//+
        {
            scale.x = Mathf.Abs(scale.x);
            //Debug.Log("오");
        }
        transform.localScale = scale;

    }

    /// <summary>
    /// 방향키 입력시 움직이는 기능
    /// </summary>
    private void PlayerMoving()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");// -1 0 1
        moveDir.y = Input.GetAxisRaw("Vertical");// -1 0 1

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// bool 형식을 사용해서 상하좌우로 입력시 애니메이션 실행
    /// </summary>
    private void RunAnim() 
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
