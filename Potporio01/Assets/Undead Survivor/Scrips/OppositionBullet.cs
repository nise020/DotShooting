using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppositionBullet : MonoBehaviour
{
    //GameManager gameManager;
    PlayerStatas playerStatas;
    Enemy enemy;
    Vector3 enPos;
    Vector3 trsPos;
    int bulletDamage = 1;
    bool on = true;

    private void OnBecameInvisible()//카메라 밖에 사라졌을때
    {
        Destroy(gameObject);//수정 가능성 있음
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Mob"))
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        playerStatas = FindObjectOfType<PlayerStatas>();
       // gameManager = GameManager.Instance;

        trsPos = transform.position;
        enPos = enemy.transform.position;

    }
    private void Update()
    {
        BulletposRotation();
        BulletposSpeed();
    }
    public void BulletposRotation() //총알방향 회전
    {
        if (on == false) { return; }
        if (on==true) 
        {
            //Vector3 trsPos = transform.position;
            Vector2 defolt = enPos - trsPos;
            float angle = Mathf.Atan2(defolt.y, defolt.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle-90);//피벗 때문에
            on = false;
        }
    }
      
    private void BulletposSpeed()
    {
        if (playerStatas == null) { return;  }

        Vector3 distance = (enPos - trsPos).normalized;
        transform.position += distance * 5.0f * Time.deltaTime;
    }
    public void BulletdeamageCheack(out int _iNum)//<-class Enemy
    {
        _iNum = bulletDamage;

    }

}
