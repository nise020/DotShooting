using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BounsBullet : MonoBehaviour
{
    float Speed = 8f;
    GameManager gamemanager;
    float roution = -140f;
    float routionEuler = 45f;
    float runTimer = 0f;
    float runTime = 10f;
    private void OnBecameInvisible()//카메라 밖에 사라졌을때
    {
        Destroy(gameObject);//수정 가능성 있음
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
        gamemanager = GameManager.Instance;
        transform.rotation = Quaternion.Euler(0, 0, routionEuler);
    }
    void Update()
    {
        transform.position += transform.up * Speed * Time.deltaTime;

        runTimer += Time.deltaTime;
        if (runTimer > runTime) { Destroy(gameObject); }
    }


}
