using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppositionBullet : MonoBehaviour
{
    GameManager gameManager;
    PlayerStatas playerStatas;
    Enemy enemy;
    AttakProces attakProces;

    Vector2 enemyTrsBe;
    Transform bulletTrnspos;
    GameObject bulletObj;
    int bulletDamage = 1;
    private void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
        enemyTrsBe = enemy.transform.localPosition;
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
        bulletTrnspos = transform;
        gameManager = FindObjectOfType<GameManager>();
        attakProces = FindObjectOfType<AttakProces>();
        bulletObj = GetComponent<GameObject>();
    }
    private void Update()
    {
       BulletposRotation();
       
    }
    private void BulletposRotation() //총알방향 회전
    {
        if (enemy == null) { return; }
        Vector2 enPos = enemyTrsBe;
        Vector2 blPos = bulletTrnspos.localPosition;
        Vector2 defolt = enPos - blPos;
        Vector2 vecUp = Vector2.up;
        float age = Vector2.Angle(vecUp, defolt);
        if (enPos.x >= 0.0 && enPos.y >= 0.0 ||
            enPos.x >= 0.0 && enPos.y <= 0.0) 
        {
            age = -Mathf.Abs(age);
        }
        bulletTrnspos.rotation = Quaternion.Euler(0,0, age);
        BulletposSpeed();
        //Debug.Log($"age={age}");

        //float age = Mathf.Atan2(bulletTrnspos.localPosition.y, enemy.transform.localPosition.x)* Mathf.Rad2Deg;
        // Mathf.Asin()
        // MathF.Sign()
        //Deg2Rad;
    }

    private void BulletposSpeed()
    {
        //if (enemy.gameObject == null) { return; }
        //float age = Mathf.Atan2(bulletTrnspos.position.y, enemy.transform.localPosition.x);
        if (playerStatas==null) { return;  }

        Vector3 enPos = enemyTrsBe;
        Vector3 blPos = bulletTrnspos.localPosition;
        Vector3 plPos;
        gameManager.PlayerTrsPosiTion(out plPos);
        Vector3 distance = plPos - enPos;
        bulletTrnspos.position = bulletTrnspos.position + enPos * 4.0f * Time.deltaTime;
        if (enemy == null) { Destroy(gameObject); }
        #region 조건 적다가 망함
        //Vector3 distance = plPos - enPos;
        //if ((distance.x > 2f  && distance.y > 2f ) == false &&
        //    (distance.x < -2f && distance.y < -2f) == false &&
        //    (distance.x > 2f  && distance.y < -2f) == false &&
        //    (distance.x < -2f && distance.y > 2f) == false )
        //{
        //    Debug.Log(distance);
        //    bulletTrnspos.position = bulletTrnspos.position + enPos * 4.0f * Time.deltaTime;


        //    if (bulletObj.transform.position.x > enemy.transform.localPosition.x &&
        //        bulletObj.transform.position.y > enemy.transform.localPosition.y) 
        //    {  Destroy(gameObject); }

        //}
        #endregion
        //Vector3 distance = trspos - bulletTrnspos.localPosition;
        //distance.z = 0.0f;
        //GameManager go = GetComponent<GameManager>();
        //gameManager.MobTrsPosiTion(out trspos);//출력용
        //한 방향으로 날아가게된 총알
    }
    public void BulletdeamageCheack(out int _iNum)//<-class Enemy
    {
        _iNum = bulletDamage;

    }

}
