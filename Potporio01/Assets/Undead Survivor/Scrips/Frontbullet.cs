using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Frontbullet : MonoBehaviour
{
    GameManager gameManager;
    PlayerStatas playerStatas;
    Transform trspos;
    // Start is called before the first frame update
    void Start()
    {
        playerStatas = FindObjectOfType<PlayerStatas>();
        trspos = transform;
        //gameManager = FindObjectOfType<GameManager>();
    }
    private void OnBecameInvisible()//ī�޶� �ۿ� ���������
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Mob"))
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        AngleCheck();
    }
    private void AngleCheck()
    {
        float age = 90;
        age = -Mathf.Abs(age);
        transform.localRotation = Quaternion.Euler(0, 0, age);
        shoot();
    }
    public void shoot()//�յڷ� �Դ� ���� ��
    {
        trspos.position += Vector3.right.normalized * 4.0f * Time.deltaTime;
    }

}
