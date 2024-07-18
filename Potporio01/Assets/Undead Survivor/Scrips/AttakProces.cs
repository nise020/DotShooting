using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakProces : MonoBehaviour
{
    Weapon weapons;//���� �������ͽ� ��ũ��Ʈ
    Enemy enemy;//��
    Transform playertransPos;
    PlayerStatas playerStatas;
    GameManager gameManager;
    AutoWeaponDron autoWeaponDron;
    //BulletManager bulletManager;
    //Bullet bullet;

    [Header("�ڵ� ����")]
    [SerializeField] bool autoCheack = false;//Ȯ�ο�
    float autoTime = 3f;
    float autoTimer = 0f;
    float AutocoolTime = 5f;
    float AutocoolTimer = 0f;
    [SerializeField] Transform autoHandRoT;

    [Header("�� ����")]
    [SerializeField] List<GameObject> autoObj;

    [Header("���� ����")]
     int swordScalecount = 0;
     int swordScaleMaxcount = 2;
     int swordUpgraidcount = 0;
     int swordUpgraidMaxcount = 3;
     int swordPlusecount = 0;
     int swordPluseMaxcount = 3;
     bool DropSwordScaleUP = false;//ũ�� ��

    [Header("���� �̹���")]
    public bool DropSwordUgaid = false;//���� ����
    public int Swordcount = 0;//������ ��

    [Header("�Ѿ� ����")]
    [SerializeField] List<GameObject> bulletKind;//�Ѿ� ����
    [SerializeField] Transform ShootPos;//�߻�� ��ġ
    [SerializeField] Transform creatTab;//�Ѿ� ���� ��
    float OpBulletCoolTimer = 0.0f;
    float DefBulletCoolTimer = 0.0f;
    float SkillBulletCoolTimer = 0.0f;
    public bool bulletOn = false;

    private void Awake()
    {
        playerStatas = GetComponent<PlayerStatas>();
        playertransPos = GetComponent<Transform>();
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("SwordScaleUP"))//ũ�� ����
        {
            DropSwordScaleUP = true;
            if (swordScalecount < swordScaleMaxcount)
            {
                weaponScale();
                swordScalecount += 1;
            }
            DropSwordScaleUP = false;

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("SwordPluse"))//���� ����
        {
            if (swordPlusecount < swordPluseMaxcount)
            {
                autoObj[Swordcount].SetActive(false);
                Swordcount += 1;
                autoObj[Swordcount].SetActive(true);
                swordPlusecount += 1;
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("SwordUgraid"))//���� ����
        {
            
            if (swordUpgraidcount < swordUpgraidMaxcount) 
            {
                DropSwordUgaid = true;
                autoObj[Swordcount].SetActive(true);
                autoWeaponDron = FindObjectOfType<AutoWeaponDron>();
                autoWeaponDron.weaponUpgraid();
                autoObj[Swordcount].SetActive(false);
                swordUpgraidcount +=1;
            }  
        }
    }
    void Update()
    {
        weaponCoolTime();//�ڵ� ����
        OpBulletCoolTime();
        DefBulletCoolTime();//�⺻ �Ѿ�,On �ʼ� 
        skillBulletCoolTime(gameManager.skillBulletOn);
    }

    public void skillBulletCoolTime(bool value)//��ų �Ѿ� ����
    {
        if (value == false) { return; }
        SkillBulletCoolTimer += Time.deltaTime;
        if (SkillBulletCoolTimer > 0.2) 
        {
            GameObject go = Instantiate(bulletKind[3], ShootPos.position,
            Quaternion.identity, creatTab.transform);
            SkillBulletCoolTimer = 0f;
        }
        
    }


    /// <summary>
    /// ������ ��Ÿ��
    /// </summary>
    private void weaponCoolTime()
    {
        if (playerStatas.DropSword == false) { return; }
        autoTimer += Time.deltaTime;
        if (autoTimer >= autoTime)
        {
            Swordcount = swordPlusecount;
            autoCheack = true;
            autoObj[Swordcount].SetActive(true);//0�� ������ �ٲ� �ʿ� ����
            AutoMod();
            AutocoolTimer += Time.deltaTime;
            if (AutocoolTimer >= AutocoolTime)
            {
                autoObj[Swordcount].SetActive(false);
                autoCheack = false;
                autoTimer = 0.0f;
                AutocoolTimer = 0.0f;
            }
        }
    }
    /// <summary>
    /// ������ ũ�� ����
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void weaponScale()
    {
        if (DropSwordScaleUP == true && swordScalecount > swordScaleMaxcount) { return; }
        if (DropSwordScaleUP == true && swordScalecount < swordScaleMaxcount)//��ư ������ true ���� 
        {
            swordScalecount += 1;//�����ʿ�

            Vector3 newScale = autoHandRoT.localScale;//���� ����
            float pluseScale = -0.5f;//��ġ
            newScale.x += Mathf.Abs(pluseScale);//����
            newScale.y += -Mathf.Abs(pluseScale);
            autoHandRoT.localScale = newScale;//�ݿ�

            DropSwordScaleUP = false;
        }
        //swordScalecount += 1;
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
            if ((playertransPos.lossyScale.x == 1)||
                (playertransPos.lossyScale.x == -1))//Scale
            {
                autoHandRoT.rotation = tRoDefolt * Defolt;

            }
            

        }

    }
    private void DefBulletCoolTime()//�Ϲ� �Ѿ˻���
    {
        DefBulletCoolTimer += Time.deltaTime;
        if (DefBulletCoolTimer > 1)
        {
            creatDefBullet();
            DefBulletCoolTimer = 0.0f;
        }
    }
    private void creatDefBullet()//�Ϲ� �Ѿ˻���
    {
        //enemy = FindObjectOfType<Enemy>();
        //if (enemy == null) { return; }//���� ������ ��� ����
        Vector3 trspos = transform.position;//�� ��ġ
        if (transform.lossyScale.x == 1)
        {
            GameObject go = Instantiate(bulletKind[1], trspos,
            Quaternion.identity, creatTab.transform);
        }
        else if(transform.lossyScale.x == -1) 
        {
            GameObject go = Instantiate(bulletKind[2], trspos,
            Quaternion.identity, creatTab.transform);
        }
        
    }

    private void OpBulletCoolTime() //�ڵ����� �����ִ� �Ѿ˻���
    {
        if (playerStatas.DropOpGun == false) { return; }
        OpBulletCoolTimer += Time.deltaTime;
        if (OpBulletCoolTimer > 1.5)
        {
            creatOpBullet();
            OpBulletCoolTimer = 0.0f;
        }
    }
    private void creatOpBullet()//�ڵ����� �����ִ� �Ѿ˻���
    {
        enemy = FindObjectOfType<Enemy>();
        if (enemy == null) { return; }//���� ������ ��� ����
        Vector3 trspos = transform.position;//�� ��ġ
        if (playerStatas.DropOpGun==true) 
        {
            GameObject go = Instantiate(bulletKind[0], trspos,
            Quaternion.identity, creatTab.transform);
        }
        
    }


}
