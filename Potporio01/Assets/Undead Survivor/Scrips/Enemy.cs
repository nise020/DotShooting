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

    [Header("몬스터 정보")]
    public int HP = 1;//퍼블릭 전환 가능성 있음
    Animator anim;
    float deathTime = 0.3f;
    float deathTimer = 0.2f;
    [SerializeField] float moveSpeed = 0.5f;//몹 이동 속도
    public bool Mobnull = false; 


    private void Awake()
    {
        MobTrnspos = transform;
        gameManager = FindObjectOfType<GameManager>();
        //지우면 ㄴㄴ
        //public 상태의 변수를 참을수 있음
        //자동 완성 기능으로 써짐
        beforeX = MobTrnspos.position.x;//기존에 x값 확인
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
    /// 데미지 계산
    /// </summary>
    private void deamageCheack()//<-class Enemy
    {
        //int MaxHP = HP;//1
        HP = HP - weapon.WeaponDamage;//class.public int = 1
        return;
        //HP = MaxHP;
        deathCheck();//if (HP <= 0)
        //이 부분은 잘 작동 합니다
    }

   /// <summary>
   /// 사망한지 체크
   /// </summary>
    private void deathCheck()
    {
        if (HP <= 0)
        {
            anim.SetBool("Death", true);
            deathTimer += Time.deltaTime;//애니메이션 재생시간
            //Destroy(gameObject);
            if (deathTimer >= deathTime)
            {
                Destroy(gameObject);
                Mobnull = true;
                deathTimer = 0.0f;
                gameManager.CreateItemCheck(transform.position);
            }
            Mobnull = false ;//위치 조정 필요
        }
        
    }

    private void Update()
    {
        Mobmoving();
        seeCheack();
        deathCheck();
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
            MobTrnspos = transform;
        }
        else 
        {
            gameManager.PlayerTrsPosiTion(out playerPos);//출력용
            Vector3 distance = playerPos - MobTrnspos.position;
            distance.z = 0.0f;
            MobTrnspos.Translate(distance.normalized * moveSpeed * Time.deltaTime);
            //Debug.Log($"{distance}");//수정필요

        }

    }
    /// <summary>
    /// 이동하는 방향에 따라서 Scale을 조절해 좌우로 이동하는것 처럼 보이게 한다
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
