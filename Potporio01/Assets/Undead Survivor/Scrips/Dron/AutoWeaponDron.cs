using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AutoWeaponDron : MonoBehaviour
{
    [SerializeField] Transform trsAuto1;
    Transform trsPos;
    Weapon weapon;
    [Header("검의 이미지")]
    [SerializeField] List<Sprite> swordSprite;//업그레이드 검의 스프라이트
    [SerializeField] List<SpriteRenderer> weaponSpriteRd;//현재 검의 스프라이트 렌더러
    [SerializeField] List<GameObject> swords;//검
    //int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    [SerializeField] public bool UpGraidBool = false;//검 이미지 변신 여부
    public int swordUpgraidcount = 1;
    public int weaponSpriteListCount = 0;
    public int swordUpgraidMaxcount = 3;
    //Image 

    private void Awake()
    {
        trsPos = trsAuto1;
        transform.parent.position = trsPos.position;//위치 초기화
    }
  
    void Update()
    {
        autocam();
        
    }
    public void weaponUpgraid()
    {
        if (swordUpgraidcount < swordUpgraidMaxcount)
        {
            int count = 10;
            for (int iNum = 0; iNum < count; iNum++) 
            {
                weaponSpriteRd[iNum].sprite = swordSprite[weaponSpriteListCount];
                weapon = swords[iNum].GetComponent<Weapon>();
                weapon.WeaponDamage += 1;
            }
            weaponSpriteListCount += 1;
            swordUpgraidcount += 1;

            //foreach (int number in numbers)
            //{
            //    weaponSpriteRd[number].sprite = swordSprite[weaponSpriteListCount];
            //}
            //Sprite sword = swordSprite[weaponSpriteListCount];
            //weaponSpriteRd[weaponSpriteListCount].sprite = sword;
            //Debug.Log($"weaponSpriteRd={weaponSpriteRd[weaponSpriteListCount].sprite}");
        }
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
