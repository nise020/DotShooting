using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakProces : MonoBehaviour
{
    Weapon Weapons;//���� �������ͽ� ��ũ��Ʈ
    Enemy enemy;//��
    Transform playertransPos;
    MoveControll moving;
    GameManager gameManager;
    //Bullet bullet;

    [Header("�ڵ� ����")]
    [SerializeField] bool autoCheack = false;//Ȯ�ο�
    float autoTime = 3f;
    float autoTimer = 0f;
    float AutocoolTime = 5f;
    float AutocoolTimer = 0f;
    float autoSpeed = 0.5f;
    float autoAngle = 360.0f;
    [SerializeField] Transform autoHandRoT;

    [Header("�� ����")]
    [SerializeField] List<GameObject> autoObj;//�⺻ ����(���� ������ �̹��� ��ȯ)
    //[SerializeField] GameObject autoObj2;//���� ���� 1��
    //[SerializeField] GameObject autoObj3;//���� ���� 2��
    //[SerializeField] GameObject autoObj4;//���� ���� 3��

    [Header("�Ѿ� ����")]
    [SerializeField] List<GameObject> bulletKind;//�Ѿ� ����
    [SerializeField] Transform creatTab;//�Ѿ� ���� ��
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
    /// ������ ��Ÿ��
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
            autoObj[0].SetActive(true);//0�� ������ �ٲ� �ʿ� ����
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
    private void creatGunBullet()//�ڵ����� �����ִ� �Ѿ˻���
    {
        enemy = FindObjectOfType<Enemy>();
        if (enemy == null) { return; }//���� ������ ��� ����
        Vector3 trspos = transform.position;//�� ��ġ
        GameObject go = Instantiate(bulletKind[0], trspos,
            Quaternion.identity, creatTab.transform);
    }
  


}
