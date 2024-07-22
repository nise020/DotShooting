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
    int bulletDamage = 1;
    bool on = true;

    private void OnBecameInvisible()//ī�޶� �ۿ� ���������
    {
        Destroy(gameObject);//���� ���ɼ� ����
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
        gameManager = GameManager.Instance;
        gameManager.PlayerTrsPosiTion(out plPos);
        enemy = FindObjectOfType<Enemy>();
       // playerStatas = FindObjectOfType<PlayerStatas>();
       // gameManager = GameManager.Instance;

        trsPos = transform.position;
        enPos = enemy.transform.position;

    }
    private void Update()
    {
        if (gameManager.objStop == true) { return; }
        BulletposRotation();
        BulletposSpeed();
    }
    public void BulletposRotation() //�Ѿ˹��� ȸ��
    {
        if (on == false) { return; }
        if (on==true) 
        {
            Vector2 defolt = enPos - trsPos;
            float angle = Mathf.Atan2(defolt.y, defolt.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle-90);//�ǹ� ������
            on = false;
            // Mathf.Atan2�� x�� �����̴�
        }
    }
      
    private void BulletposSpeed()
    {
        if (plPos == null) { return; }
        //GameObject go = Random.Range(0, count);
        Vector3 distance = (enPos - trsPos).normalized;
        transform.position += distance * 5.0f * Time.deltaTime;
    }
    public void BulletdeamageCheack(out int _iNum)//<-class Enemy
    {
        _iNum = bulletDamage;

    }

}
