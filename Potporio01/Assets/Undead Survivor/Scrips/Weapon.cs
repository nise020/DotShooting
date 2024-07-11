using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    PlayerStatas playerStatas;
    AttakProces attakProces;

    [Header("검의 이미지")]
    [SerializeField] List < Sprite> swordSprite;//업그레이드 검의 스프라이트
    public SpriteRenderer weaponSpriteRd;//현재 검의 스프라이트 렌더러
    [SerializeField] public bool UpGraidBool = false;//검 이미지 변신 여부
    public int swordUpgraidcount = 1;
    public int weaponSpriteListCount = 0;
    public int swordUpgraidMaxcount = 3;


    [Header("검의 크기")]
    [SerializeField] public bool WeaponScaleCheack = false;//검의 크기 여부
    Transform trsWeapon;//검의 상태,크기조정
    public int swordScalecount = 0;
    public int swordScaleMaxcount = 2;


    [Header("검의 데미지")]
    public int WeaponDamage = 1;
    public int WeaponMaxDamage = 4;//미정
    int WeaponDamageMaxcount = 1;
    //UpgaidBool = false;
    //weaponScaleCheack = false;

    private void Awake()
    {
        weaponSpriteRd = GetComponent<SpriteRenderer>();
        trsWeapon = GetComponent<Transform>();
        attakProces = FindObjectOfType<AttakProces>();
    }

    /// <summary>
    /// 데미지 계산
    /// </summary>
    public void WeapondeamageCheack(out int _iNum)//<-class Enemy
    {
        _iNum = WeaponDamage;

    }


    private void Update()
    {
        //weaponScale();
        //weaponUpgraid();
    }

    /// <summary>
    /// 무기의 스프라이트 이미지를 변경
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void weaponUpgraid()
    {
        //swordUpgraidcount += 1;//조정필요
        if (attakProces.DropSwordUgaid == false) { return; }
        if (attakProces.DropSwordUgaid == true && swordUpgraidcount > swordUpgraidMaxcount) { return; }

        if (attakProces.DropSwordUgaid == true && swordUpgraidcount < swordUpgraidMaxcount)
        {
            swordUpgraidcount += 1;//조정필요

            weaponSpriteRd.sprite = swordSprite[weaponSpriteListCount];
            weaponSpriteListCount += 1;
            WeaponDamage += 1;
            Debug.Log($"weaponSpriteRd={weaponSpriteRd}");
        }
        attakProces.DropSwordUgaid = false;
    }


    /// <summary>
    /// 무기의 크기 증가
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void weaponScale()
    {
        if (attakProces.DropSwordUgaid == true && swordScalecount > swordScaleMaxcount) { return; }
        if (attakProces.DropSwordUgaid == true && swordScalecount < swordScaleMaxcount)//버튼 누를시 true 예정 
        {
            swordScalecount += 1;//조정필요

            Vector3 newScale = trsWeapon.localScale;//변수 지정
            float pluseScale = -0.5f;//수치
            newScale.x += -Mathf.Abs(pluseScale);//증감
            newScale.y += Mathf.Abs(pluseScale);
            trsWeapon.localScale = newScale;//반영

            WeaponScaleCheack = false;
        }
        //swordScalecount += 1;
    }

}
