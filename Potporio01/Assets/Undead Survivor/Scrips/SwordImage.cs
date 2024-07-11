using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordImage : MonoBehaviour
{
    PlayerStatas playerStatas;
    AttakProces attakProces;

    [Header("검의 이미지")]
    [SerializeField] List<Sprite> swordSprite;//업그레이드 검의 스프라이트
    SpriteRenderer[] weaponSpriteRd;//현재 검의 스프라이트 렌더러
    [SerializeField] public bool UpGraidBool = false;//검 이미지 변신 여부
    public int swordUpgraidcount = 1;
    public int weaponSpriteListCount = 0;
    public int swordUpgraidMaxcount = 3;

    private void Awake()
    {
        //gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        weaponSpriteRd = GetComponentsInChildren<SpriteRenderer>();
        //weaponSpriteRd = Resources.FindObjectsOfTypeAll<SpriteRenderer>();
        attakProces = FindObjectOfType<AttakProces>();
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
        if (swordUpgraidcount < swordUpgraidMaxcount) 
        {
            Sprite sword = swordSprite[weaponSpriteListCount];
            weaponSpriteRd[weaponSpriteListCount].sprite = sword;
            Debug.Log($"weaponSpriteRd={weaponSpriteRd[weaponSpriteListCount].sprite}");
            weaponSpriteListCount += 1;
            swordUpgraidcount +=1;
        }
        

    }

}
