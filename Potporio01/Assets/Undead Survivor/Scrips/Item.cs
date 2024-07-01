using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    GameManager gameManager;
    PlayerStatas playerStatas;
    public enum itemType
    {
        Heal,
        SpeedUp,
    }
    [SerializeField] itemType ItemKind;
    [SerializeField] float moveSpeed = 0.5f;
    Transform Trnspos;
    private void Awake()
    {
        Trnspos = transform;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
            if (playerStatas.MgItem == true)
            {
                Destroy(gameObject);
                ItemMoving();
            }
            
        }
    }
    public void ItemMoving()
    {
        Vector3 playerPos;

        if (gameManager.trsTarget == null)//player가 죽었을 경우
        {
            Trnspos = transform;
        }
        else
        {
            gameManager.PlayerTrsPosiTion(out playerPos);//출력용
            Vector3 distance = playerPos - Trnspos.position;
            distance.z = 0.0f;
            Trnspos.Translate(distance.normalized * moveSpeed * Time.deltaTime);
            //Debug.Log($"{distance}");//수정필요

        }

    }
}
