using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppositionBullet : MonoBehaviour
{
    GameManager gameManager;
    PlayerStatas playerStatas;
    Enemy enemy;
    Vector3 enPos;
    Vector3 trsPos;
    Vector3 plPos;
    Transform bulletTrnspos;
    int bulletDamage = 1;
    bool on = true;
    private void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
        playerStatas = FindObjectOfType<PlayerStatas>();
    }
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
        //bulletTrnspos = transform;
        gameManager = GameManager.Instance;
        //transform.position = gameManager.trsTarget.position;
        enPos = enemy.transform.position;// position;
        trsPos = transform.position;
        plPos = playerStatas.transform.position;

        //attakProces = FindObjectOfType<AttakProces>();
        //bulletObj = GetComponent<GameObject>();
    }
    private void Update()
    {
        BulletposRotation();
        BulletposSpeed();
    }
    public void BulletposRotation() //총알방향 회전
    {
        if (enemy == null || on == false) { return; }
        if (on==true) 
        {
            //Vector3 enPos = enemy.transform.position;
            //gameManager.PlayerTrsPosiTion(out Vector3 blPos);
            Vector2 defolt = enPos - plPos;
            //Vector2 vecUp = Vector2.up;
            //float age = Vector2.Angle(vecUp, defolt);
            float angle = Mathf.Atan2(defolt.y, defolt.x) * Mathf.Rad2Deg;
            if (enPos.x >= 0.0 && enPos.y >= 0.0 ||
                enPos.x >= 0.0 && enPos.y <= 0.0)
            {
                //age = -Mathf.Abs(age);
                angle = -Mathf.Abs(angle);
            }
            //else if(enPos.x <= 0.0 && enPos.y >= 0.0)
            //{
            //    //age = Mathf.Abs(age);
            //    angle = Mathf.Abs(angle);
            //}
            //transform.rotation = Quaternion.Euler(0, 0, age);
            transform.rotation = Quaternion.Euler(0, 0, angle);
            on = false;
            Debug.Log(angle);
        }
    }
        

    private void BulletposSpeed()
    {
        if (playerStatas==null) { return;  }

        Vector3 distance = (enPos - trsPos).normalized;
        transform.position += distance * 5.0f * Time.deltaTime;
    }
    public void BulletdeamageCheack(out int _iNum)//<-class Enemy
    {
        _iNum = bulletDamage;

    }

}
