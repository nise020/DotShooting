using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static TagorDataList;

public class PlayerStatas : MonoBehaviour
{
    Enemy enemy;
    HpCanvers hpCanvers;
    Animator anim;
    MoveControll moveControll;
    //GameManager gameManager;

    [Header("플레이어 정보")]
    [SerializeField, Range(1, 10)] int plLevel = 1;//레벨,화면에 표시되게 하고 싶다
    public float NowHp = 5;//시각적으로 보이는 현재 체력
    public float MaximumHP = 5;//최대 체력
    int beforHP;
    [SerializeField] int score = 0;//점수
    float deathTime = 1.0f;//사망처리 시간
    float deathTimer = 0.0f;//
    [Header("플레이어 정보")]
    public bool Alive = true;//생존 여부
    public bool DropGun = false;
    public bool DropDfGun = false;//자동 조준 드랍 여부
    //[SerializeField] float expGaigi = 0.0f;//경험치 포인트
    //[SerializeField] float MaiximumExpGaigi = 10.0f;//경험치 최대치
    //int magnetDrop = 0;//자력 업그레이드
    //public bool MagnetItem = false;//자력ON
    //public bool DropGun = false;
    //public bool DropGun = false;

    //CircleCollider2D mgcircle;
 
    private void OnValidate()
    {
        //beforHP
        //NowHp = MaximumHP;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Mob"))//무적시간 구현 예정
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null) 
            {
                int damage = 0;
                enemy.MobDemageCheack(out damage);
                NowHp -= damage;
                //GameManager.Instance.Hpcheck(NowHp, MaximumHP);//일단 삭제 ㄴㄴ
                Debug.Log($"NowHp={NowHp}");
            }
            //if (NowHp <= MaximumHP)//데미지
            //{

            //}
            //else { return; }
        }
        //if (collision.gameObject.layer == LayerMask.NameToLayer("EXP"))//item
        //{
        //    expGaigi += 1.0f;//경험치 증가
        //    //Debug.Log($"expGaigip={expGaigi}");
        //    if (expGaigi > MaiximumExpGaigi)
        //    {
        //        expGaigi = 0.0f;//경험치 초기화
        //        MaiximumExpGaigi += MaiximumExpGaigi;//경험치 최대치 증가
        //        Debug.Log($"MaiximumExpGaigi={MaiximumExpGaigi}");
        //    }
        //    else { return; }
        //}
        if (collision.gameObject.layer == LayerMask.NameToLayer("Heal"))//item
        {
            if (NowHp < MaximumHP)//힐
            {
                NowHp += 1;
                //GameManager.Instance.Hpcheck(NowHp, MaximumHP);
                Debug.Log($"NowHp={NowHp}");
            }
            else { return; }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Speed"))//스피드
        {
            moveControll = GetComponent<MoveControll>();
            if (moveControll.MaxiumSpeed > moveControll.moveSpeed)
            {
                moveControll.moveSpeed += 0.5f;
            }
            else { return; }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("SwordScaleUP"))//크기 증가
        {

        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("SwordUgaid"))//무기 변신
        {

        }
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Magnet"))//경험치 빨아들이기
        //{
        //    MagnetItem = true;
        //    Destroy(collision.gameObject);
        //    if (MagnetItem==true) 
        //    {
        //        MagnetItem = false;
        //        //expDeta.DrainEXP();
        //    }
        //    ////MgItem = true;
        //    //if (MgItem == true) 
        //    //{
        //    //    mgcircle.radius += 0.1f;
        //    //    MgItem = false;
        //    //}
        //}
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
        if (NowHp <= 0 && Alive == true)
        {
            //Alive = false;
            anim = GetComponent<Animator>();
            anim.SetBool("Death", true);
            deathTimer += Time.deltaTime;
            Debug.Log(deathTimer);
            if (deathTimer > deathTime)
            {
                Alive = false;
                Destroy(gameObject);

                //gameObject.SetActive(false);//수정 필요
            }
        }
        
    }
}
