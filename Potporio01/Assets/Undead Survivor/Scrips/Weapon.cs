using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Transform trsWeapon;
    private void Awake()
    {
        trsWeapon = GetComponent<Transform>();
    }
    
}
