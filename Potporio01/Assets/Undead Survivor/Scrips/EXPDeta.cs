using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EXPDeta : MonoBehaviour
{
    //Transform trsEXPpos;
    //PlayerStatas playerStatas;
    //GameManager gameManager;
    //Enemy enemy;
    //public CircleCollider2D RaiderCircle;//레이더
    //[SerializeField] Transform trsPlayerpos;//플레이어
    //[SerializeField] List<Vector2> RaiderLine;//0~5
    //[SerializeField] bool on = true;
    //float drainSpeed = 0.2f;
    //float dainTmier = 0.0f;
    //float dainTmie = 0.2f;
    //BoxCollider2D box;

    //private void Awake()
    //{
    //    box = GetComponent<BoxCollider2D>();
    //    trsEXPpos = transform;//내 위치 상태
    //    //trsEXP = transform.GetChild(0);
    //    //trsEXP.position = transform.position;
    //    RaiderCircle = GetComponent<CircleCollider2D>();
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    //private void Update()
    //{
    //    //if (trsEXP.position != gameObject.transform.position) { DrainEXP(); }
    //    //DrainEXP();
    //}


    //public void DrainEXP()
    //{

    //    Vector3 player;
    //    player = trsPlayerpos.position;
    //    Vector3 distance = player - trsEXPpos.position;
    //    distance.z = 0.0f;
    //    trsEXPpos.Translate(distance.normalized * drainSpeed * Time.deltaTime);



    //    #region dranins
    //    Vector2 dranin1 = new Vector2(0.2f, 0.0f);   //1.0,0.0
    //    Vector2 dranin2 = new Vector2(-0.2f, 0.0f);  //-1.0,0.0
    //    Vector2 dranin3 = new Vector2(0.0f, 0.2f);   //0.0,1.0
    //    Vector2 dranin4 = new Vector2(0.0f, -0.2f);  //0.0,-1.0
    //    Vector2 dranin5 = new Vector2(0.2f, 0.2f);   //1.0,1.0f
    //    Vector2 dranin6 = new Vector2(-0.2f, -0.2f); //-1.0,-1.0
    //    Vector2 dranin7 = new Vector2(0.2f, -0.2f);  //1.0,-1.0
    //    Vector2 dranin8 = new Vector2(0.2f, 0.2f);   //-1.0,1.0

    //    dranin1.x += transform.position.x;
    //    dranin1.y += transform.position.y;

    //    dranin2.x -= transform.position.x;
    //    dranin2.y += transform.position.y;

    //    dranin3.x += transform.position.x;
    //    dranin3.y += transform.position.y;

    //    dranin4.x += transform.position.x;
    //    dranin4.y -= transform.position.y;

    //    dranin5.x += transform.position.x;
    //    dranin5.y += transform.position.y;

    //    dranin6.x -= transform.position.x;
    //    dranin6.y -= transform.position.y;

    //    dranin7.x += transform.position.x;
    //    dranin7.y -= transform.position.y;

    //    dranin8.x -= transform.position.x;
    //    dranin8.y -= transform.position.y;
    //    #endregion
    //    player = trsPlayerpos.position;

    //    #region
    //    //1.0,0.0
    //    //- 1.0,0.0
    //    //0.0,1.0
    //    //0.0,-1.0
    //    //1.0,1.0f
    //    //- 1.0,-1.0
    //    //1.0,-1.0
    //    //- 1.0,1.0

    //    //* drainSpeed
    //    Debug.Log(distance);
    //    //(5.0, -10, 0)
    //    #endregion
    //    if ((player.x >= dranin1.x && player.y >= dranin1.y) == true ||
    //        (player.x <= dranin2.x && player.y >= dranin2.y) == true ||
    //        (player.x >= dranin3.x && player.y >= dranin3.y) == true ||
    //        (player.x >= dranin4.x && player.y <= dranin4.y) == true ||
    //        (player.x >= dranin5.x && player.y >= dranin5.y) == true ||
    //        (player.x <= dranin6.x && player.y <= dranin6.y) == true ||
    //        (player.x >= dranin7.x && player.y <= dranin7.y) == true ||
    //        (player.x <= dranin8.x && player.y >= dranin8.y) == true)
    //    {


    //        //}
    //        #region Debug.Log
    //        //Debug.Log($"dranin1 = {dranin1}");
    //        //Debug.Log($"dranin2 = {dranin2}");
    //        //Debug.Log($"dranin3 = {dranin3}");
    //        //Debug.Log($"dranin4 = {dranin4}");
    //        //Debug.Log($"dranin5 = {dranin5}");
    //        //Debug.Log($"dranin6 = {dranin6}");
    //        //Debug.Log($"dranin7 = {dranin7}");
    //        //Debug.Log($"dranin8 = {dranin8}");
    //        #endregion
    //        on = false;

    //    }

    //}
}
