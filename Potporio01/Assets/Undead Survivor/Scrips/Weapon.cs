using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    PlayerStatas playerStatas;
    AttakProces attakProces;

    [Header("���� �̹���")]
    [SerializeField] List < Sprite> swordSprite;//���׷��̵� ���� ��������Ʈ
    public SpriteRenderer weaponSpriteRd;//���� ���� ��������Ʈ ������
    [SerializeField] public bool UpGraidBool = false;//�� �̹��� ���� ����
    public int swordUpgraidcount = 1;
    public int weaponSpriteListCount = 0;
    public int swordUpgraidMaxcount = 3;


    [Header("���� ũ��")]
    [SerializeField] public bool WeaponScaleCheack = false;//���� ũ�� ����
    Transform trsWeapon;//���� ����,ũ������
    public int swordScalecount = 0;
    public int swordScaleMaxcount = 2;


    [Header("���� ������")]
    [SerializeField] public int WeaponDamage = 1;
    public int WeaponMaxDamage = 4;//����
    int WeaponDamageMaxcount = 1;
    //UpgaidBool = false;
    //weaponScaleCheack = false;

    private void Awake()
    {
        attakProces = FindObjectOfType<AttakProces>();
    }

    /// <summary>
    /// ������ ���
    /// </summary>
    public void WeapondeamageCheack(out int _iNum)//<-class Enemy
    {
        _iNum = WeaponDamage + attakProces.Swordcount;

    }

    /// <summary>
    /// ������ ��������Ʈ �̹����� ����
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void weaponUpgraid()
    {
        //swordUpgraidcount += 1;//�����ʿ�
        if (attakProces.DropSwordUgaid == false) { return; }
        if (attakProces.DropSwordUgaid == true && swordUpgraidcount > swordUpgraidMaxcount) { return; }

        if (attakProces.DropSwordUgaid == true && swordUpgraidcount < swordUpgraidMaxcount)
        {
            swordUpgraidcount += 1;//�����ʿ�

            weaponSpriteRd.sprite = swordSprite[weaponSpriteListCount];
            weaponSpriteListCount += 1;
            WeaponDamage += 1;
            Debug.Log($"weaponSpriteRd={weaponSpriteRd}");
        }
        attakProces.DropSwordUgaid = false;
    }
}
