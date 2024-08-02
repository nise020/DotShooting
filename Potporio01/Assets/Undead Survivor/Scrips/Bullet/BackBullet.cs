using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBullet : MonoBehaviour
{
    GameManager gameManager;
    Transform trspos;
    float Speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        //Speed = Speed;
        trspos = transform;
        gameManager = GameManager.Instance;
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
        if (gameManager.objStop == true) { return; }
        AngleCheck();
    }
    private void AngleCheck()
    {
        float age = 90;
        age = Mathf.Abs(age);
        transform.localRotation = Quaternion.Euler(0, 0, age);
        shoot();
    }
    public void SppedUP(float value)
    {
        Speed = value + Speed;
    }
    public void shoot()//�յڷ� �Դ� ���� ��
    {
        trspos.position += Vector3.left * Speed * Time.deltaTime;
    }
}
