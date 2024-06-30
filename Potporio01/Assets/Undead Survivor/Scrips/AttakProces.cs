using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakProces : MonoBehaviour
{
    Weapon Weapons;
    Transform playertransPos;
    moveControll moving;
    GameManager gameManager;

    [Header("자동 공격")]
    [SerializeField] bool autoCheack = false;
    [SerializeField] float autoTime = 3.0f;
    float autoTimer = 0.0f;
    float autoSpeed = 0.5f;
    float autoAngle = 360.0f;
    [SerializeField] Transform autoHandRoT;
    [SerializeField] GameObject autoObj;

    [Header("수동 공격")]
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

    private void modCheack()
    {
        autoTimer += Time.deltaTime;
        if (autoTimer > autoTime)
        {
            manulCheack = false;
            autoCheack = true;
            manulcoolTimer = 0.0f;
            autoObj.SetActive(true);
            manulObj.SetActive(false);
            AutoMod();
        }
        if (Input.GetKeyDown(KeyCode.Space) || 
            Input.GetKeyDown(KeyCode.LeftControl))
        {
            manulCheack = true;
            autoCheack = false;
            autoTimer = 0.0f;
            autoObj.SetActive(false);
            manulObj.SetActive(true);
            manualMod();

        }
    }

    /// <summary>
    /// 자동 공격
    /// </summary>
    private void AutoMod()
    {
        if (autoCheack == true) 
        {
            float speed = 10.0f;
            Quaternion tRoDefolt = autoHandRoT.rotation;
            Quaternion Defolt = Quaternion.Euler(0, 0, 30f * speed * Time.deltaTime);//z값을 늘리면 회전 속도가 올라간다
            Debug.Log(tRoDefolt);
            if ((playertransPos.localScale.x == 1)||
                (playertransPos.localScale.x == -1))//Scale
            {
                //Defolt.z = Mathf.Abs(Defolt.z);
                autoHandRoT.rotation = tRoDefolt * Defolt;
                Debug.Log(autoHandRoT.rotation.z);
                //Debug.Log(tRoDefolt);
            }
            //Debug.Log(autoHandRoT.rotation.z);

        }
        
    }
    /// <summary>
    ///  수동 공격
    /// </summary>
    private void manualMod()
    {
        //manulcoolTime = manulcoolTime * Time.deltaTime;
        if (manulCheack == true)
        {      
            manulcoolTimer = manulcoolTimer * Time.deltaTime;
            //float speed = 10.0f;
            Quaternion tRoDefolt = autoHandRoT.rotation;
            Quaternion Defolt = new Quaternion();
            Defolt = Quaternion.Euler(0, 0, 60f * Time.deltaTime);
            autoHandRoT.rotation = Defolt* tRoDefolt;
            if (manulcoolTimer> manulcoolTime) 
            {
                manulHandPos.rotation = beForemanulHand;
            }
            //manulHandPos.transform.localScale = Vector3.one;
            //float Angle = Quaternion.FromToRotation(Vector3.up,Vector3.right).eulerAngles.z;//각도,xyzw
            //Debug.Log($"beFore Angle = {Angle}");
            //manulHandPos.rotation = Quaternion.Euler(0, 0, speed * Angle * Time.deltaTime);//단순 베기
            //manulHandPos.rotation = Quaternion.EulerRotation(0, 0, speed * Angle * Time.deltaTime);//단순 베기
            //Debug.Log($" Angle = {Angle}");
            //manulCheack = true;
            //playertransPos.localRotation = Quaternion.identity;

            //Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(mouseWorldPos);
            //Vector2 playerPos = transform.position;
            //Vector2 fixedPos = mouseWorldPos - playerPos;

        }
         

    }
}
