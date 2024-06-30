using System;
using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    
    GameManager gameManager;
    Vector3 moveDir;
    [SerializeField] float moveSpeed = 0.5f;//몹 이동 속도
    private Transform mobTrnspos;
    private float beforeX;
    CapsuleCollider2D mobCollider;

    [Header("몬스터 정보")]
    int HP = 1;
    Animator anim;
    float deathTime = 0.5f;
    float deathTimer = 0.0f;


    private void Awake()
    {
        mobTrnspos = transform;
        gameManager = FindObjectOfType<GameManager>();
        //public 상태의 변수를 참을수 있음
        //자동 완성 기능으로 써짐
        beforeX = mobTrnspos.position.x;//기존에 x값 확인
        mobCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }
    

    private void Update()
    {
        Mobmoving();
        seeCheack();
        death();

    }

    private void death()
    {
        if (HP == 0)//사망시
        {
            anim.SetBool("Death", true);
            deathTimer += Time.deltaTime;
            //Destroy(gameObject);
            if (deathTimer > deathTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            HP = HP - 1;//임시
        }
    }
    private void Atteak()//폐기예정
    {
        //임시기능
        if (mobCollider.IsTouchingLayers(LayerMask.GetMask("Player")) == true) 
        {
            Destroy(gameObject);
        }
    }



    /// <summary>
    /// 플레이어가 있는 방향으로 자동으로 이동합니다
    /// 보스를 추가하면 사용 예정(주의! public임)
    /// </summary>
    public void Mobmoving() 
    {
        Vector3 playerPos;
       
        if (gameManager.trsTarget == null)//player가 죽었을 경우
        {
            return;
        }
        else 
        {
            gameManager.PlayerTrsPosiTion(out playerPos);//출력용
            Vector3 distance = playerPos - mobTrnspos.position;
            distance.z = 1.0f;
            mobTrnspos.Translate(distance.normalized * moveSpeed * Time.deltaTime);
            //Debug.Log($"{distance}");//수정필요

        }

    }
    /// <summary>
    /// 이동하는 방향에 따라서 Scale을 조절해 좌우로 이동하는것 처럼 보이게 한다
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
