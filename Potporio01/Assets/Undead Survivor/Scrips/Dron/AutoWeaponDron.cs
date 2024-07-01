using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeaponDron : MonoBehaviour
{
    [SerializeField] Transform trsAuto1;
    //[SerializeField] Transform egoWeapon1;//미정

   

    // Update is called once per frame
    void Update()
    {
        autocam();

    }
    /// <summary>
    /// 플레이어를 따라다니는 기능
    /// </summary>
    private void autocam()
    {
        Vector3 fixpos = trsAuto1.position;
        fixpos.z = transform.position.z;
        transform.position = fixpos;
    }
}
