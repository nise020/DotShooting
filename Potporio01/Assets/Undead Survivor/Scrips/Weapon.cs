using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

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
    [SerializeField] public int WeaponDamage = 1;
    public int WeaponMaxDamage = 4;//미정
    int WeaponDamageMaxcount = 1;


    /// <summary>
    /// 데미지 계산
    /// </summary>
    public void WeapondeamageCheack(out int _iNum)//<-class Enemy
    {
        _iNum = WeaponDamage;

    }

}
