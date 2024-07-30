using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakProces : MonoBehaviour
{
    Enemy enemy;//��
    Transform playertransPos;
    PlayerStatas playerStatas;
    GameManager gameManager;

    [Header("�ڵ� ����")]
    bool autoCheack = false;//Ȯ�ο�
    float autoTime = 3f;
    float autoTimer = 0f;
    float AutocoolTime = 5f;
    float AutocoolTimer = 0f;
    [SerializeField] Transform autoHandRoT;

    [Header("�� ����")]
    [SerializeField] List<GameObject> autoObj;

    [Header("���� ����")]
    public int swordScalecount = 0;
    public int swordScaleMaxcount = 3;
    public int swordUpgraidcount = 0;
    public int swordUpgraidMaxcount = 3;
    public int swordPlusecount = 0;
    public int swordPluseMaxcount = 3;
     bool DropSwordScaleUP = false;//ũ�� ��

    [Header("���� �̹���")]
    public bool DropSwordUgaid = false;//���� ����
    public int Swordcount = 0;//������ ��

    [Header("�Ѿ� ����")]
    [SerializeField] List<GameObject> bulletKind;//�Ѿ� ����
    [SerializeField] Transform ShootPos;//�߻�� ��ġ
    [SerializeField] Transform creatTab;//�Ѿ� ���� ��
    [SerializeField] Transform GunHandTab;//�Ѿ� ���� ��
    float OpBulletCoolTimer = 0.0f;
    float DefBulletCoolTimer = 0.0f;
    float SkillBulletCoolTimer = 0.0f;
    float BounsBulletCoolTimer = 0.0f;
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet")) { return; }

        Item item = collision.gameObject.GetComponent<Item>();
        string name = "";
        if (collision.gameObject.layer == LayerMask.NameToLayer("SwordScaleUP"))//ũ�� ����
        {
            item.GetItenType(out name);
            DropSwordScaleUP = true;
            if (swordScalecount < swordScaleMaxcount)
            {
                weaponScale();
                swordScalecount += 1;
            }
            else { gameManager.buffKind.RemoveAt(gameManager.buffKindNmber); }
            DropSwordScaleUP = false;
            gameManager.Tooltip(name);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("SwordPluse"))//���� ���� ����
        {
            item.GetItenType(out name);
            if (swordPlusecount < swordPluseMaxcount)
            {
                autoObj[Swordcount].SetActive(false);
                Swordcount += 1;
                autoObj[Swordcount].SetActive(true);
                swordPlusecount += 1;
            }
            else { gameManager.buffKind.RemoveAt(gameManager.buffKindNmber); }
            gameManager.Tooltip(name);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("SwordUgraid"))//���� ����
        {
            item.GetItenType(out name);
            if (swordUpgraidcount < swordUpgraidMaxcount)
            {
                DropSwordUgaid = true;
                autoObj[Swordcount].SetActive(true);
                AutoWeaponDron autoWeaponDron = autoHandRoT.gameObject.GetComponent<AutoWeaponDron>();
                autoWeaponDron.weaponUpgraid();
                autoObj[Swordcount].SetActive(false);
                swordUpgraidcount += 1;
            }
            else { gameManager.buffKind.RemoveAt(gameManager.buffKindNmber); }
            gameManager.Tooltip(name);
            Destroy(collision.gameObject);
        }
    }
    void Update()
    {
        if (gameManager.objStop == true) { return; }
        weaponCoolTime();//�ڵ� ����
        OpBulletCoolTime();
        DefBulletCoolTime();//�⺻ �Ѿ�,On �ʼ� 
        BounsBulletCoolTime();
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
        Vector3 newScale = autoHandRoT.localScale;//���� ����
        float pluseScale = -0.5f;//��ġ
        newScale.x += Mathf.Abs(pluseScale);//����
        newScale.y += -Mathf.Abs(pluseScale);
        autoHandRoT.localScale = newScale;//�ݿ�
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
    private void DefBulletCoolTime()//�Ϲ� �Ѿ� Ÿ�̸�
    {
        DefBulletCoolTimer += Time.deltaTime;
        if (DefBulletCoolTimer > 0.5)
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
            GameObject go = Instantiate(bulletKind[1], ShootPos.position,
            Quaternion.identity, creatTab.transform);
        }
        else if(transform.lossyScale.x == -1) 
        {
            GameObject go = Instantiate(bulletKind[2], ShootPos.position,
            Quaternion.identity, creatTab.transform);
        }
        
    }

    private void OpBulletCoolTime() //�ڵ����� �����ִ� �Ѿ� Ÿ�̸�
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
        if (playerStatas.DropOpGun==true) 
        {
            GameObject go = Instantiate(bulletKind[0], ShootPos.position,
            Quaternion.identity, creatTab.transform);
        }
        
    }
    private void BounsBulletCoolTime() //�ٿ �Ѿ� Ÿ�̸�
    {
        if (playerStatas.DropBoundGun == false) { return; }
        BounsBulletCoolTimer += Time.deltaTime;
        if (BounsBulletCoolTimer > 3.0)
        {
            creatBounsBullet();
            BounsBulletCoolTimer = 0.0f;
        }
    }
    private void creatBounsBullet()//�ٿ �Ѿ� �Ѿ˻���
    {
        if (playerStatas.DropBoundGun == true)
        {
            GameObject go = Instantiate(bulletKind[4], ShootPos.position,
            Quaternion.identity, GunHandTab.transform);
        }

    }

}
