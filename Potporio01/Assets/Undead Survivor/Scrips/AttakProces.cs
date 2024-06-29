using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AttakProces : MonoBehaviour
{
    Transform playertransPos;
    moveControll moving;
    GameManager gameManager;

    [Header("자동 공격")]
    [SerializeField] bool autoMod = false;
    [SerializeField] float autoTime = 2.0f;
    float autoTimer = 0.0f;
    float autoSpeed = 0.5f;
    float autoAngle = 360.0f;
    [SerializeField] Transform trsHandRoT;

    [Header("수동 공격")]
    [SerializeField] bool attackceck = false;
    [SerializeField] Transform trsHandPos;
    private void Awake()
    {
        //playertransPos = transform;
        playertransPos = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        AutoMod();
        //AttackMod();
    }

    private void AutoMod() //수정필요
    {
        autoTimer = Time.time; 
        if (autoTimer > autoTime) 
        {
            autoMod = true;
        }
        if (autoMod == true) 
        {      
            float afterScale = playertransPos.localScale.x;
            Quaternion tRoDefolt = trsHandRoT.rotation;
            Quaternion Defolt = new Quaternion();
            Defolt = Quaternion.Euler(0, 0, 360f * Time.deltaTime);
            if ((playertransPos.localScale.x == -1) && 
                (playertransPos.localScale.x != 0))//Scale
            {
                Defolt.z = -Mathf.Abs(180.0f);
                trsHandRoT.rotation = tRoDefolt * Defolt;
                Debug.Log(trsHandRoT.rotation.z);
            }
            else 
            {
                trsHandRoT.rotation = tRoDefolt * Defolt;
                Debug.Log(trsHandRoT.rotation.z);
            }

        }
        
    }
    private void AttackMod()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.Mouse1)) //우클릭 임시
        {
            autoMod = false;
            autoTimer = 0.0f;
            attackceck = true;
            //playertransPos.localRotation = Quaternion.identity;

            //Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(mouseWorldPos);
            Vector2 playerPos = transform.position;
            //Vector2 fixedPos = mouseWorldPos - playerPos;

            //float Angle = Quaternion.FromToRotation(Vector3.up,Vector3.right).eulerAngles.z;//각도,xyzw
            //trsHandPos.rotation = Quaternion.Euler(0, 0, Angle);//단순 베기
        }
    }
}
