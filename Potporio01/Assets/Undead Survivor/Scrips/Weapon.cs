using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

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


    /// <summary>
    /// ������ ���
    /// </summary>
    public void WeapondeamageCheack(out int _iNum)//<-class Enemy
    {
        _iNum = WeaponDamage;

    }

}
