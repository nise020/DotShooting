using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    Enemy enemy;
    [SerializeField] bool weaponScaleCheack = false;
    private Vector2 upgraid;
    Transform trsWeapon;//���� ����,ũ������
    Image image;//���� ��������Ʈ
    public int WeaponDamage = 1;
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
        //weaponUpgraid();
        //weaponPluse();
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

    }

    /// <summary>
    /// ���⸦ �ִ� 4������ ����
    /// </summary>
    private void weaponPluse()
    {

    }

    /// <summary>
    /// ������ ũ�� ����
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void weaponScale()
    {
        if (weaponScaleCheack==true)//��ư ������ true ���� 
        {
            Vector2 newScale = transform.localScale;//���� ����
            float pluseScale = -0.5f;//��ġ
            newScale.x += -Mathf.Abs(pluseScale);//����
            newScale.y += Mathf.Abs(pluseScale);
            transform.localScale = newScale;//�ݿ�
            weaponScaleCheack = false;
        }
    }
    
}
