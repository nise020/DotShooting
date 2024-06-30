using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAutoDron : MonoBehaviour
{
    [SerializeField] Transform trsAuto;
   

    // Update is called once per frame
    void Update()
    {
        Vector3 fixpos = trsAuto.position;
        fixpos.z = transform.position.z;
        transform.position = fixpos;
    }

    private void autocam()
    {
        Vector3 fixpos = trsAuto.position;
        fixpos.z = transform.position.z;
        transform.position = fixpos;
    }
}
