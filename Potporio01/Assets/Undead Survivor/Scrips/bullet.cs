using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //shoot();
    }
    //private void shoot() 
    //{
    //    Vector3 pos;
    //    gameManager.PlayerTrsLoPos(out pos);
    //    if (pos.x == 1)
    //    {
    //        transform.position += Vector3.right* 4 * Time.deltaTime;
    //    }
    //    else 
    //    {
    //        transform.position += Vector3.left * Time.deltaTime;
    //    }
    //}
}
