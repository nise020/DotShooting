using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    Enemy enomy;
    [Header("드랍 아이템 종류")]
    [SerializeField] List <GameObject> ItemKind;//1.힐/2.이속증가//3.
    [SerializeField] GameObject Experience;//경험치
    [SerializeField] List<GameObject> Mobkind;//몹의 종류
    //
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CreateItem();
    }

    private void CreateItem()
    {
        if (ItemKind == null)//확률 부분
        {
        }
    }
}
