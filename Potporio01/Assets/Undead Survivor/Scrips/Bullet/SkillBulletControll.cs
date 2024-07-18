using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillBulletControll : MonoBehaviour
{
    float Speed = 5f;
    Vector3 defolt;
    GameObject bullet;
    void Start()
    {
        GameManager gameManager = GameManager.Instance;
        transform.parent = null;
        transform.localScale = gameManager.trsTarget.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        int count = transform.childCount;
        if (count == 0) 
        {
            Destroy(gameObject);
        }
    }
}
