using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBullet : MonoBehaviour
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
    private void OnBecameInvisible()//카메라 밖에 사라졌을때
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
        // Scale = playerStatas.transform.lossyScale;
        age = Mathf.Abs(age);
        transform.localRotation = Quaternion.Euler(0, 0, age);
        shoot();
    }
    public void shoot()//앞뒤로 왔다 갔다 함
    {
        //Vector3 pos = transform.localPosition;
        //Vector3 Scale = playerStatas.transform.lossyScale;
        //distance = (Vector3.up - transform.position).normalized;
        trspos.position += Vector3.left.normalized * 4.0f * Time.deltaTime;
        //trspos.Translate(Vector3.up.normalized * 4.0f * Time.deltaTime);

        //if (age == 90)
        //{
        //    //float pos = 1;
        //    //pos = -Mathf.Abs(pos);
        //    trspos.Translate(Vector3.down.normalized * 4.0f * Time.deltaTime);
        //}
        //else 
        //{
        //    //float pos = 1;
        //    trspos.Translate(Vector3.up.normalized * 4.0f * Time.deltaTime);
        //    //trspos.position += new Vector3(-1, 0, 0) * 4.0f * Time.deltaTime;
        //}
        //trspos.Translate(new Vector3(0, pos, 0).normalized * 4.0f * Time.deltaTime);
        //trspos.position += new Vector3(pos,0,0) * 4.0f * Time.deltaTime;
    }
}
