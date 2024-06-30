using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDron : MonoBehaviour
{
    [SerializeField] Transform trsCam;


    void Update()
    {

        Vector3 fixpos = trsCam.position;
        fixpos.z = transform.position.z;
        transform.position = fixpos;
    }

    private void Camera()
    {
        Vector3 fixpos = trsCam.position;
        fixpos.z = transform.position.z;
        transform.position = fixpos;
    }
}
