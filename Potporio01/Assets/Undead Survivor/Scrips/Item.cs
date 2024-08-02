using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    GameManager gameManager;
    public enum ItemType
    {
        Sword,
        Heal,
        SpeedUp,
        OpGun,
        MaxHpUp,
        SwordPluse,
        SwordScaleUP,
        SwordUgraid,
        BoundGun,
    }
    float timer = 0;
    [SerializeField] public ItemType Type;//¸÷ Å¸ÀÔ
    //[SerializeField] Image Itemimage;
    SpriteRenderer SpriteRenderer;
    private void Start()
    {
        gameManager = GameManager.Instance;
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10f) 
        {
            Destroy(gameObject);
        }
    }
 
    public string GetItenType(out string name) 
    {
        name = Type.ToString();
        return name;
    }
}
