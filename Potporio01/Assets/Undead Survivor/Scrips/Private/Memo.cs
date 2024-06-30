using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memo : MonoBehaviour
{
    //Transform[] childs =
    //viewInventory.transform.GetComponentsInChildren<Transform>();

    //groundCheckColl = GetComponentInChildren<BoxCollider2D>();
    //자식(GroundCheck)의 BoxCollider2D를 찾는다
    //groundCheckColl = GetComponentInParent<BoxCollider2D>();
    //부모의 BoxCollider2D를 찾는다


    //fabExplosion = Resources.Load<GameObject>("Effect/Test/fabExplosion");
    //fabExplosion = gameobject
    private void inItJason()
    {
        //TextAsset itemData = Resources.Load("ItemData") as TextAsset;
        //itemDatas = JsonConvert.DeserializeObject<List<cItemDate>>(itemData.ToString());

        //이렇게 사용 가능
        //itemData = (TextAsset)Resources.Load("ItemData");
        //itemData = Resources.Load<TextAsset>("ItemData");//null

        //확장자 입력ㄴㄴ/폴더명/ItemData<-파일명


    }


    //Physics2D.BoxCast(box2d.bounds.center, box2d.bounds.size,
    //0f,Vector2.down, groundCheckLenght, LayerMask.GetMask("Ground"));
}
