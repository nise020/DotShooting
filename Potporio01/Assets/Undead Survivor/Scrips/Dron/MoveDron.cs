using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDron : MonoBehaviour
{
    PlayerStatas playerStatas;
    [SerializeField] Transform trsCam;
    [SerializeField] GameObject playerObj;

    private void Start()
    {
        playerStatas = playerObj.GetComponent<PlayerStatas>();
    }
    void Update()
    {
        Camera();
    }

    private void Camera()
    {
        if (playerObj == null) { return; }
        Vector3 fixpos = trsCam.position;
        fixpos.z = transform.position.z;
        transform.position = fixpos;
    }
}
