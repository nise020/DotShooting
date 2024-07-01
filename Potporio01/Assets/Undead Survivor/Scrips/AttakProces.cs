using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakProces : MonoBehaviour
{
    Weapon Weapons;//무기 스테이터스 스크립트
    Transform playertransPos;
    moveControll moving;
    GameManager gameManager;

    [Header("자동 공격")]
    [SerializeField] bool autoCheack = false;//확인용
    [SerializeField] float autoTime = 3.0f;
    float autoTimer = 0.0f;
    float autoSpeed = 0.5f;
    float autoAngle = 360.0f;
    [SerializeField] Transform autoHandRoT;

    [Header("무기 종류")]
    [SerializeField] GameObject autoObj1;//기본 무기(무기 렙업시 이미지 전환)
    [SerializeField] GameObject autoObj2;//무기 증식 1렙
    [SerializeField] GameObject autoObj3;//무기 증식 2렙
    [SerializeField] GameObject autoObj4;//무기 증식 3렙

    [Header("수동 공격")]//없앨까 고민중
    [SerializeField] bool manulCheack = false;
    [SerializeField] Transform manulHandPos;
    [SerializeField] GameObject manulObj;
    Quaternion beForemanulHand;
    float manulcoolTime = 0.5f;
    float manulcoolTimer = 0.0f;
    private void Awake()
    {
        //playertransPos = transform;
        beForemanulHand = manulHandPos.rotation;
        playertransPos = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        modCheack();
        
    }
    /// <summary>
    /// 공격 방식을 정한다
    /// </summary>
    private void modCheack()//수동 공격 없애면 수정 필요함
    {
        autoTimer += Time.deltaTime;
        if (autoTimer > autoTime)//수동 공격 없애면 수정 필요함
        {
            manulCheack = false;
            autoCheack = true;
            manulcoolTimer = 0.0f;
            autoObj1.SetActive(true);
            //autoObj2.SetActive(true);
            //autoObj3.SetActive(true);
            //autoObj4.SetActive(true);
            //manulObj.SetActive(false);
            AutoMod();
        }
        if (Input.GetKeyDown(KeyCode.Space) || 
            Input.GetKeyDown(KeyCode.LeftControl))//수동 공격 없애면 수정 필요함
        {
            manulCheack = true;
            autoCheack = false;
            autoTimer = 0.0f;
            autoObj1.SetActive(false);
            //autoObj2.SetActive(false);
            //autoObj3.SetActive(false);
            //autoObj4.SetActive(false);
            //manulObj.SetActive(true);
            //manualMod();

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
                //Debug.Log(autoHandRoT.rotation.z);
                //Debug.Log(tRoDefolt);
            }
            //Debug.Log(autoHandRoT.rotation.z);

        }
        
    }
    /// <summary>
    ///  수동 공격
    /// </summary>
    private void manualMod()//삭제 고민중,애초에 미완성
    {
        if (manulCheack == true)
        {      
            manulcoolTimer = manulcoolTimer * Time.deltaTime;
            Quaternion tRoDefolt = autoHandRoT.rotation;
            Quaternion Defolt = new Quaternion();
            Defolt = Quaternion.Euler(0, 0, 60f * Time.deltaTime);
            autoHandRoT.rotation = Defolt* tRoDefolt;
            if (manulcoolTimer> manulcoolTime) 
            {
                manulHandPos.rotation = beForemanulHand;
            }

        }
         

    }
}
