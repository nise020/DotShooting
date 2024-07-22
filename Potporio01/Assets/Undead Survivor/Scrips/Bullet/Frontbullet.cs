using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Frontbullet : MonoBehaviour
{
    GameManager gameManager;
    Transform trspos;
    // Start is called before the first frame update
    void Start()
    {
        trspos = transform;
        gameManager = GameManager.Instance;
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
        if (gameManager.objStop == true) { return; }
        AngleCheck();
    }
    private void AngleCheck()
    {
        float age = 90;
        age = -Mathf.Abs(age);
        transform.localRotation = Quaternion.Euler(0, 0, age);
        shoot();
    }
    public void shoot()
    {
        trspos.position += Vector3.right * 4.0f * Time.deltaTime;
    }

}
