using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    Enemy enemy;
    [SerializeField] bool weaponScaleCheack = false;
    private Vector2 upgraid;
    Transform trsWeapon;//검의 상태,크기조정
    Image image;//검의 스프라이트
    public int WeaponDamage = 5;
    BoxCollider2D hiteEria;

    private void Awake()
    {
        image = GetComponent<Image>();
        trsWeapon = GetComponent<Transform>();
        hiteEria = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        weaponScale();
       // weaponUpgraid();
    }

    /// <summary>
    /// 데미지 계산기
    /// </summary>
    /// <param name="_iNum"></param>
    /// <returns>int _iNum</returns>
    public int DamageNumber(int _iNum)
    {
        _iNum -= WeaponDamage;//public int = 5(필드)
        Debug.Log(_iNum);
        return _iNum;
    }
    /// <summary>
    /// 무기의 스프라이트 이미지를 변경
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void weaponUpgraid()
    {

    }
    /// <summary>
    /// 무기의 크기 증가
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void weaponScale()
    {
        if (weaponScaleCheack==true)//버튼 누를시 true 예정 
        {
            Vector2 newScale = transform.localScale;//변수 지정
            float pluseScale = -0.5f;//수치
            newScale.x += -Mathf.Abs(pluseScale);//증감
            newScale.y += Mathf.Abs(pluseScale);
            transform.localScale = newScale;//반영
            weaponScaleCheack = false;
        }
    }
    
}
