using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MobBullet : MonoBehaviour
{
    PlayerStatas playerStatas;
    Vector3 plPos;
    Vector3 trsPos;
    SpriteRenderer spriteRenderer;
    float runtimer = 0f;
    float runtime = 3f;
    bool on = true;
    private void Awake()
    {
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
        BulletposRotation();
        BulletposSpeed();
        dilliteRun();
    }
    private void BulletposRotation() //총알방향 회전
    {
        if (on == false) { return; }
        if (on == true)
        {
            //Vector3 trsPos = transform.position;
            Vector2 defolt = plPos - trsPos;
            float angle = Mathf.Atan2(defolt.y, defolt.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);//피벗 때문에
            on = false;
            // Mathf.Atan2는 x축 기준이다
        }
    }
    private void dilliteRun()//일정시간이 지난후 삭제
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
