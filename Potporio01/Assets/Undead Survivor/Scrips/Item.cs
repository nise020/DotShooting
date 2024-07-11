using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Item : MonoBehaviour
{
    //GameManager gameManager;
    //PlayerStatas playerStatas;
    //public enum itemType
    //{
    //    Heal,
    //    SpeedUp,
    //}
    //[SerializeField] itemType ItemKind;
    //[SerializeField] float moveSpeed = 0.5f;
    //Transform Trnspos;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
        }

    }

}
