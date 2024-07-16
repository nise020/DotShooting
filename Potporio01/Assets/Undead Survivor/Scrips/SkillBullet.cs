using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBullet : MonoBehaviour
{
    Transform betrsPos;
    float Speed = 2f;
    Vector3 defolt;
    void Start()
    {
        betrsPos= transform;
    }

    // Update is called once per frame
    void Update()
    {
        defolt.x += betrsPos.position.x;
        defolt.y += betrsPos.position.y;

        transform.localPosition += defolt.normalized * Speed * Time.deltaTime;
    }
}
