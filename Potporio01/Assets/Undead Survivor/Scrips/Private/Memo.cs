using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memo : MonoBehaviour
{
    //Transform[] childs =
    //viewInventory.transform.GetComponentsInChildren<Transform>();

    //groundCheckColl = GetComponentInChildren<BoxCollider2D>();
    //�ڽ�(GroundCheck)�� BoxCollider2D�� ã�´�
    //groundCheckColl = GetComponentInParent<BoxCollider2D>();
    //�θ��� BoxCollider2D�� ã�´�


    //fabExplosion = Resources.Load<GameObject>("Effect/Test/fabExplosion");
    //fabExplosion = gameobject
    private void inItJason()
    {
        //TextAsset itemData = Resources.Load("ItemData") as TextAsset;
        //itemDatas = JsonConvert.DeserializeObject<List<cItemDate>>(itemData.ToString());

        //�̷��� ��� ����
        //itemData = (TextAsset)Resources.Load("ItemData");
        //itemData = Resources.Load<TextAsset>("ItemData");//null

        //Ȯ���� �Է¤���/������/ItemData<-���ϸ�


    }


    //Physics2D.BoxCast(box2d.bounds.center, box2d.bounds.size,
    //0f,Vector2.down, groundCheckLenght, LayerMask.GetMask("Ground"));
}
