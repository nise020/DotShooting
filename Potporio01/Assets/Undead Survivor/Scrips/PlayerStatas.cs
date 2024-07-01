using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatas : MonoBehaviour
{
    MoveControll moveControll;

    [Header("플레이어 정보")]
    [SerializeField, Range(1, 10)] int plLevel = 1;//레벨,화면에 표시되게 하고 싶다
    [SerializeField] float exp = 0.0f;//경험치 시작시점
    [SerializeField] float fullExp = 10.0f;//경험치 최대치
    [SerializeField] float ExpPoint = 1.0f;//경험치 포인트
    [SerializeField, Range(0, 5)] public int NowHp = 5;//시가적으로 보이는 체력

    private int MaximumHP = 5;
    float deathTime = 1.0f;
    float deathTimer = 0.0f;
    Animator anim;
    public bool Alive =true;//생존 여부
    Transform trsmagnet;
    int magnetDrop = 0;
    public bool MgItem = false;
    CircleCollider2D mgcircle;
    private void Awake()
    {
        trsmagnet = transform;
        mgcircle = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Mob"))//무적시간 구현 예정
        {
            if (NowHp <= MaximumHP)//데미지
            {
                NowHp = NowHp - 1;//고정뎀    
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Heal"))//item
        {
            if (NowHp <= MaximumHP)//힐
            {
                NowHp += 1;
                Debug.Log(NowHp);
            }
            else 
            {
                NowHp = MaximumHP;//체력 오버 막기
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Speed"))//item
        {

        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Magnet"))//item
        {
            MgItem = true;
            if (MgItem == true) 
            {
                mgcircle.radius += 0.1f;
                MgItem = false;
            }
        }
    }

    //collision.tag == Tool.GetTag(GameTags.Item)
    private void Update()
    {
        deathCheck();
    }
    /// <summary>
    /// 사망시 연출
    /// </summary>
    private void deathCheck() //트리거 필요
    {
        if (NowHp == 0 && Alive == true)
        {
            //Alive = false;
            anim = GetComponent<Animator>();
            anim.SetBool("Death", true);
            deathTimer += Time.deltaTime;
            Debug.Log(deathTimer);
            if (deathTimer >= deathTime)
            {
                gameObject.SetActive(false);//수정 필요
                Alive = false;
            }
        }
        
    }
}
