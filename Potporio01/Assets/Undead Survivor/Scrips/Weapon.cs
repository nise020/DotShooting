using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    PlayerStatas playerStatas;

    [Header("���� �̹���")]
    [SerializeField] List < Sprite> swordSprite;//���׷��̵� ���� ��������Ʈ
    SpriteRenderer weaponSpriteRd;//���� ���� ��������Ʈ ������
    [SerializeField] public bool UpgaidBool = false;//�� �̹��� ���� ����
    public int swordUpgraidcount = 0;
    public int weaponSpriteListCount = 0;
    public int swordUpgraidMaxcount = 3;


    [Header("���� ũ��")]
    [SerializeField] public bool WeaponScaleCheack = false;//���� ũ�� ����
    Transform trsWeapon;//���� ����,ũ������
    public int swordScalecount = 0;
    public int swordScaleMaxcount = 2;


    [Header("���� ������")]
    public int WeaponDamage = 1;
    public int WeaponMaxDamage = 4;//����
    int WeaponDamageMaxcount = 1;
    //UpgaidBool = false;
    //weaponScaleCheack = false;

    private void Awake()
    {
        weaponSpriteRd = GetComponent<SpriteRenderer>();
        trsWeapon = GetComponent<Transform>();
    }
    
    /// <summary>
    /// ������ ���
    /// </summary>
    public void WeapondeamageCheack(out int _iNum)//<-class Enemy
    {
        _iNum = WeaponDamage;

    }


    private void Update()
    {
        weaponScale();
        weaponUpgraid();
    }


    /// <summary>
    /// ������ ����
    /// </summary>
    /// <param name="_iNum"></param>
    /// <returns>int _iNum</returns>
    public int DamageFigure(int _iNum)
    {
        _iNum -= WeaponDamage;//public int = 1(�ʵ�)
        Debug.Log(_iNum);
        return _iNum;
    }
    /// <summary>
    /// ������ ��������Ʈ �̹����� ����
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void weaponUpgraid()
    {
        if (UpgaidBool == true && swordUpgraidcount > swordUpgraidMaxcount) { return; }

        if (UpgaidBool == true && swordUpgraidcount < swordUpgraidMaxcount) 
        {
            swordUpgraidcount += 1;//�����ʿ�

            weaponSpriteRd.sprite = swordSprite[weaponSpriteListCount];
            weaponSpriteListCount += 1;
            
            UpgaidBool = false;
        }
        //swordUpgraidcount += 1;
    }


    /// <summary>
    /// ������ ũ�� ����
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void weaponScale()
    {
        if (WeaponScaleCheack == true && swordScalecount > swordScaleMaxcount) { return; }
        if (WeaponScaleCheack == true && swordScalecount < swordScaleMaxcount)//��ư ������ true ���� 
        {
            swordScalecount += 1;//�����ʿ�

            Vector2 newScale = trsWeapon.localScale;//���� ����
            float pluseScale = -0.5f;//��ġ
            newScale.x += -Mathf.Abs(pluseScale);//����
            newScale.y += Mathf.Abs(pluseScale);
            trsWeapon.localScale = newScale;//�ݿ�

            WeaponScaleCheack = false;
        }
        //swordScalecount += 1;
    }

}
