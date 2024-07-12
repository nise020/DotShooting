using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MobBullet : MonoBehaviour
{
    GameManager gameManager;
    PlayerStatas playerStatas;
    Enemy enemy;
    Vector3 plPos;
    Vector3 trsPos;
    SpriteRenderer spriteRenderer;
    float runtimer = 0f;
    float runtime = 5f;
    private void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
        playerStatas = FindObjectOfType<PlayerStatas>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnBecameInvisible()//카메라 밖에 사라졌을때
    {
        Destroy(gameObject);//수정 가능성 있음
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        plPos = playerStatas.transform.position;// position;
        trsPos = transform.position;
    }
    private void Update()
    {
        BulletposSpeed();
        dilliteRun();
    }

    private void dilliteRun()
    {
        runtimer += Time.deltaTime;
        if (runtimer > runtime) 
        {
            Color color = spriteRenderer.color;
            color.a -= Time.deltaTime / runtime;
            if (color.a < 0) 
            {
                Destroy(gameObject);
            }
            spriteRenderer.color = color;
        }
    }

    private void BulletposSpeed()
    {
        Vector3 distance = (plPos - trsPos).normalized;
        transform.position += distance * 1.0f * Time.deltaTime;
    }

}
