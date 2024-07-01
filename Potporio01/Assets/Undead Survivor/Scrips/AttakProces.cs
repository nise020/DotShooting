using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakProces : MonoBehaviour
{
    Weapon Weapons;//���� �������ͽ� ��ũ��Ʈ
    Transform playertransPos;
    moveControll moving;
    GameManager gameManager;

    [Header("�ڵ� ����")]
    [SerializeField] bool autoCheack = false;//Ȯ�ο�
    [SerializeField] float autoTime = 3.0f;
    float autoTimer = 0.0f;
    float autoSpeed = 0.5f;
    float autoAngle = 360.0f;
    [SerializeField] Transform autoHandRoT;

    [Header("���� ����")]
    [SerializeField] GameObject autoObj1;//�⺻ ����(���� ������ �̹��� ��ȯ)
    [SerializeField] GameObject autoObj2;//���� ���� 1��
    [SerializeField] GameObject autoObj3;//���� ���� 2��
    [SerializeField] GameObject autoObj4;//���� ���� 3��

    [Header("���� ����")]//���ٱ� �����
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
    /// ���� ����� ���Ѵ�
    /// </summary>
    private void modCheack()//���� ���� ���ָ� ���� �ʿ���
    {
        autoTimer += Time.deltaTime;
        if (autoTimer > autoTime)//���� ���� ���ָ� ���� �ʿ���
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
            Input.GetKeyDown(KeyCode.LeftControl))//���� ���� ���ָ� ���� �ʿ���
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
    /// �ڵ� ����
    /// </summary>
    private void AutoMod()
    {
        if (autoCheack == true) 
        {
            float speed = 10.0f;//���� ����� �ð� ����(���� �Ǹ�)
            Quaternion tRoDefolt = autoHandRoT.rotation;
            Quaternion Defolt = Quaternion.Euler(0, 0, 30f * speed * Time.deltaTime);
            //z���� �ø��� ȸ�� �ӵ��� �ö󰣴�
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
    ///  ���� ����
    /// </summary>
    private void manualMod()//���� �����,���ʿ� �̿ϼ�
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
