using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeaponDron : MonoBehaviour
{
    [SerializeField] Transform trsAuto1;
    //[SerializeField] Transform trsPlayer;
    //[SerializeField] Transform egoWeapon1;//����

    Transform trsPos;
    //SpriteRenderer weaponSpriteRd;//���� ���� ��������Ʈ ������
    //[SerializeField] List<Sprite> swordSprite;//���׷��̵� ���� ��������Ʈ
    //int swordUpgraidcount = 0;
    //[SerializeField] bool UpgaidBool = false;



    private void Awake()
    {
        trsPos = trsAuto1;
        transform.parent.position = trsPos.position;//��ġ �ʱ�ȭ
        //weaponSpriteRd = GetComponentInChildren<SpriteRenderer>();

    }
  
    void Update()
    {
        autocam();
    }

    /// <summary>
    /// �÷��̾ ����ٴϴ� ���
    /// </summary>
    public void autocam()
    {
        if (trsAuto1==null) { return; }
        Vector3 fixpos = trsAuto1.position;
        fixpos.z = transform.position.z;
        transform.position = fixpos;
    }

}
