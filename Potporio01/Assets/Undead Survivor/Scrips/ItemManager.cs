using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    Enemy enomy;
    [Header("��� ������ ����")]
    [SerializeField] List <GameObject> ItemKind;//1.��/2.�̼�����//3.
    [SerializeField] GameObject Experience;//����ġ
    [SerializeField] List<GameObject> Mobkind;//���� ����
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
        if (ItemKind == null)//Ȯ�� �κ�
        {
        }
    }
}
