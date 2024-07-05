using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeaponDron : MonoBehaviour
{
    [SerializeField] Transform trsAuto1;
    //[SerializeField] Transform trsPlayer;
    //[SerializeField] Transform egoWeapon1;//미정

    Transform trsPos;
    //SpriteRenderer weaponSpriteRd;//현재 검의 스프라이트 렌더러
    //[SerializeField] List<Sprite> swordSprite;//업그레이드 검의 스프라이트
    //int swordUpgraidcount = 0;
    //[SerializeField] bool UpgaidBool = false;



    private void Awake()
    {
        trsPos = trsAuto1;
        transform.parent.position = trsPos.position;//위치 초기화
        //weaponSpriteRd = GetComponentInChildren<SpriteRenderer>();

    }
  
    void Update()
    {
        autocam();
    }

    /// <summary>
    /// 플레이어를 따라다니는 기능
    /// </summary>
    public void autocam()
    {
        if (trsAuto1==null) { return; }
        Vector3 fixpos = trsAuto1.position;
        fixpos.z = transform.position.z;
        transform.position = fixpos;
    }

}
