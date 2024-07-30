using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakProces : MonoBehaviour
{
    Enemy enemy;//몹
    Transform playertransPos;
    PlayerStatas playerStatas;
    GameManager gameManager;

    [Header("자동 공격")]
    bool autoCheack = false;//확인용
    float autoTime = 3f;
    float autoTimer = 0f;
    float AutocoolTime = 5f;
    float AutocoolTimer = 0f;
    [SerializeField] Transform autoHandRoT;

    [Header("검 종류")]
    [SerializeField] List<GameObject> autoObj;

    [Header("검의 상태")]
    public int swordScalecount = 0;
    public int swordScaleMaxcount = 3;
    public int swordUpgraidcount = 0;
    public int swordUpgraidMaxcount = 3;
    public int swordPlusecount = 0;
    public int swordPluseMaxcount = 3;
     bool DropSwordScaleUP = false;//크기 업

    [Header("검의 이미지")]
    public bool DropSwordUgaid = false;//무기 변신
    public int Swordcount = 0;//지금의 검

    [Header("총알 공격")]
    [SerializeField] List<GameObject> bulletKind;//총알 종류
    [SerializeField] Transform ShootPos;//발사될 위치
    [SerializeField] Transform creatTab;//총알 생성 탭
    [SerializeField] Transform GunHandTab;//총알 생성 탭
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("SwordScaleUP"))//크기 증가
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
        else if (collision.gameObject.layer == LayerMask.NameToLayer("SwordPluse"))//무기 개수 증가
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
        else if (collision.gameObject.layer == LayerMask.NameToLayer("SwordUgraid"))//무기 증가
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
        weaponCoolTime();//자동 무기
        OpBulletCoolTime();
        DefBulletCoolTime();//기본 총알,On 필수 
        BounsBulletCoolTime();
        skillBulletCoolTime(gameManager.skillBulletOn);
    }

    public void skillBulletCoolTime(bool value)//스킬 총알 생성
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
    /// 무기의 쿨타임
    /// </summary>
    private void weaponCoolTime()
    {
        if (playerStatas.DropSword == false) { return; }
        autoTimer += Time.deltaTime;
        if (autoTimer >= autoTime)
        {
            Swordcount = swordPlusecount;
            autoCheack = true;
            autoObj[Swordcount].SetActive(true);//0을 변수로 바꿀 필요 있음
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
    /// 무기의 크기 증가
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void weaponScale()
    {
        Vector3 newScale = autoHandRoT.localScale;//변수 지정
        float pluseScale = -0.5f;//수치
        newScale.x += Mathf.Abs(pluseScale);//증감
        newScale.y += -Mathf.Abs(pluseScale);
        autoHandRoT.localScale = newScale;//반영
        //swordScalecount += 1;
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
            if ((playertransPos.lossyScale.x == 1)||
                (playertransPos.lossyScale.x == -1))//Scale
            {
                autoHandRoT.rotation = tRoDefolt * Defolt;

            }
            

        }

    }
    private void DefBulletCoolTime()//일반 총알 타이머
    {
        DefBulletCoolTimer += Time.deltaTime;
        if (DefBulletCoolTimer > 0.5)
        {
            creatDefBullet();
            DefBulletCoolTimer = 0.0f;
        }
    }
    private void creatDefBullet()//일반 총알생성
    {
        //enemy = FindObjectOfType<Enemy>();
        //if (enemy == null) { return; }//몹이 없을때 사용 안함
        Vector3 trspos = transform.position;//내 위치
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

    private void OpBulletCoolTime() //자동으로 맞춰주는 총알 타이머
    {
        if (playerStatas.DropOpGun == false) { return; }
        OpBulletCoolTimer += Time.deltaTime;
        if (OpBulletCoolTimer > 1.5)
        {
            creatOpBullet();
            OpBulletCoolTimer = 0.0f;
        }
    }
    private void creatOpBullet()//자동으로 맞춰주는 총알생성
    {
        enemy = FindObjectOfType<Enemy>();
        if (enemy == null) { return; }//몹이 없을때 사용 안함
        if (playerStatas.DropOpGun==true) 
        {
            GameObject go = Instantiate(bulletKind[0], ShootPos.position,
            Quaternion.identity, creatTab.transform);
        }
        
    }
    private void BounsBulletCoolTime() //바운스 총알 타이머
    {
        if (playerStatas.DropBoundGun == false) { return; }
        BounsBulletCoolTimer += Time.deltaTime;
        if (BounsBulletCoolTimer > 3.0)
        {
            creatBounsBullet();
            BounsBulletCoolTimer = 0.0f;
        }
    }
    private void creatBounsBullet()//바운스 총알 총알생성
    {
        if (playerStatas.DropBoundGun == true)
        {
            GameObject go = Instantiate(bulletKind[4], ShootPos.position,
            Quaternion.identity, GunHandTab.transform);
        }

    }

}
