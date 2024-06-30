using System;
using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    
    GameManager gameManager;
    Vector3 moveDir;
    [SerializeField] float moveSpeed = 0.5f;
    private Transform mobTrnspos;
    private float beforeX;
    CapsuleCollider2D mobCollider;
    private void Awake()
    {
        mobTrnspos = transform;
        gameManager = FindObjectOfType<GameManager>();
        beforeX = mobTrnspos.position.x;//기존에 x값 확인
        mobCollider = GetComponent<CapsuleCollider2D>();
    }


    private void Update()
    {
        Mobmoving();
        seeCheack();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
        }
    }
    private void Atteak()
    {
        //임시기능
        if (mobCollider.IsTouchingLayers(LayerMask.GetMask("Player")) == true) 
        {
            Destroy(gameObject);
        }
    }



    /// <summary>
    /// 플레이어가 있는 방향으로 자동으로 이동합니다
    /// </summary>
    private void Mobmoving() 
    {
        Vector3 playerPos;
       
        if (gameManager.trsTarget == null)//player가 죽었을 경우
        {
            return;
        }
        else 
        {
            gameManager.PlayerLocalPosiTion(out playerPos);//출력용
            Vector3 distance = playerPos - mobTrnspos.position;
            distance.z = 1.0f;
            mobTrnspos.Translate(distance.normalized * moveSpeed * Time.deltaTime);
            //Debug.Log($"{distance}");//수정필요

        }

    }
    /// <summary>
    /// 이동하는 방향에 따라서 Scale을 조절해 좌우로 이동하는것 처럼 보이게 한다
    /// </summary>
    private void seeCheack()
    {
        Vector3 scale = mobTrnspos.localScale;
        float affterX = mobTrnspos.position.x;
       
        if (affterX > beforeX)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else if (affterX < beforeX)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
        beforeX = affterX;

    }
}
