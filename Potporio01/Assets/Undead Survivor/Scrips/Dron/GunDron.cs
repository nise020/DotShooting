using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDron : MonoBehaviour
{
    
    [SerializeField] Transform trsPos;
    
    private void Start()
    {
        transform.parent.position = trsPos.position;//��ġ �ʱ�ȭ
    }
    // Update is called once per frame
    void Update()
    {
        if (trsPos == null) { return; }
        Vector3 fixpos = trsPos.position;
        fixpos.z = transform.position.z;
        transform.position = fixpos;
    }
}
