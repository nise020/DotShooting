using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakProces : MonoBehaviour
{
    Weapon Weapons;//무기 스테이터스 스크립트
    Enemy enemy;//몹
    Transform playertransPos;
    MoveControll moving;
    GameManager gameManager;
    //Bullet bullet;

    [Header("자동 공격")]
    [SerializeField] bool autoCheack = false;//확인용
    float autoTime = 3f;
    float autoTimer = 0f;
    float AutocoolTime = 5f;
    float AutocoolTimer = 0f;
    float autoSpeed = 0.5f;
    float autoAngle = 360.0f;
    [SerializeField] Transform autoHandRoT;

    [Header("검 종류")]
    [SerializeField] List<GameObject> autoObj;//기본 무기(무기 렙업시 이미지 전환)
    //[SerializeField] GameObject autoObj2;//무기 증식 1렙
    //[SerializeField] GameObject autoObj3;//무기 증식 2렙
    //[SerializeField] GameObject autoObj4;//무기 증식 3렙

    [Header("총알 공격")]
    [SerializeField] List<GameObject> bulletKind;//총알 종류
    [SerializeField] Transform creatTab;//총알 생성 탭
    Transform bulletTrnspos;
    float BulletCoolTimer = 0.0f;
    bool bulletCool = false;
    public bool bulletOn = false;

    private void Awake()
    {
        //playertransPos = transform;
        //beForemanulHand = manulHandPos.rotation;
        playertransPos = GetComponent<Transform>();
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        weaponCoolTime();
        BulletCoolTime();
    }
    /// <summary>
    /// 무기의 쿨타임
    /// </summary>
    private void weaponCoolTime()
    {
        //autoObj1.SetActive(false);
        //autoCheack = false;
        autoTimer += Time.deltaTime;
        //Debug.Log(autoTime);
        if (autoTimer >= autoTime)
        {
            autoCheack = true;
            autoObj[0].SetActive(true);//0을 변수로 바꿀 필요 있음
            AutoMod();
            AutocoolTimer += Time.deltaTime;
            if (AutocoolTimer >= AutocoolTime)
            {
                autoObj[0].SetActive(false);
                autoCheack = false;
                autoTimer = 0.0f;
                AutocoolTimer = 0.0f;
            }
        }
    }

    /// <summary>
    /// 자동 공격
    /// </summary>
    private void AutoMod()
    {
        if (autoCheack == true) 
        {
            float speed = 10.0f;//버프 적용시 시간 단축(여유 되면)
            Quaternion tRoDefolt = autoHandRoT.rotation;
            Quaternion Defolt = Quaternion.Euler(0, 0, 30f * speed * Time.deltaTime);
            //z값을 늘리면 회전 속도가 올라간다
            if ((playertransPos.localScale.x == 1)||
                (playertransPos.localScale.x == -1))//Scale
            {
                autoHandRoT.rotation = tRoDefolt * Defolt;

            }
            

        }

    }
    
    private void BulletCoolTime() 
    {
        BulletCoolTimer += Time.deltaTime;
        if (BulletCoolTimer > 0.6 ) 
        {
            creatGunBullet();
            BulletCoolTimer = 0.0f;
        }
    }
    private void creatGunBullet()//자동으로 맞춰주는 총알생성
    {
        enemy = FindObjectOfType<Enemy>();
        if (enemy == null) { return; }//몹이 없을때 사용 안함
        Vector3 trspos = transform.position;//내 위치
        GameObject go = Instantiate(bulletKind[0], trspos,
            Quaternion.identity, creatTab.transform);
    }
  


}
