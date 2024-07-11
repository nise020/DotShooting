using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordImage : MonoBehaviour
{
    PlayerStatas playerStatas;
    AttakProces attakProces;

    [Header("���� �̹���")]
    [SerializeField] List<Sprite> swordSprite;//���׷��̵� ���� ��������Ʈ
    SpriteRenderer[] weaponSpriteRd;//���� ���� ��������Ʈ ������
    [SerializeField] public bool UpGraidBool = false;//�� �̹��� ���� ����
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
    /// ������ ��������Ʈ �̹����� ����
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
