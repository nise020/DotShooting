using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BounsBullet : MonoBehaviour
{
    float Speed = 8f;
    GameManager gameManager;
    float roution = -140f;
    float routionEuler = 45f;
    float runTimer = 0f;
    float runTime = 10f;
    private void OnBecameInvisible()//ī�޶� �ۿ� ���������
    {
        Destroy(gameObject);//���� ���ɼ� ����
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GunHand") 
        {
            transform.Rotate(0, 0, roution);
        }
    }
    void Start()
    {
        gameManager = GameManager.Instance;
        transform.rotation = Quaternion.Euler(0, 0, routionEuler);
    }
    void Update()
    {
        if (gameManager.objStop == true) { return; }
        transform.position += transform.up * Speed * Time.deltaTime;

        runTimer += Time.deltaTime;
        if (runTimer > runTime) { Destroy(gameObject); }
    }


}
