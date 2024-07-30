
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour//메모용
{
    Camera cam;
    [SerializeField] BoxCollider2D coll;
    [SerializeField] Transform trsPlayer;//
    Bounds curBound;
    public enum enumData

    {
        Skull,
        Defolt,
        White,

    }
    //[SerializeField] enumData Type;//몹 타입
    private void Start()
    {
        int enumcount = System.Enum.GetValues(typeof(enumData)).Length;
        int randNum = Random.Range(0, 4);
        enumData randEnum = (enumData)randNum;//enum 데이터를 int데이터를 활용해서 랜덤으로 부여

        cam =Camera.main;
        CheckBound();
    }
    private void Update()
    {
        if (trsPlayer == null) { return; }
        cam.transform.position = new Vector3(
            Mathf.Clamp(trsPlayer.position.x,curBound.min.x, curBound.max.x),
            Mathf.Clamp(trsPlayer.position.y,curBound.min.y, curBound.max.y),
            cam.transform.position.z);
    }

    private void CheckBound()
    {
        float Hight = cam.orthographicSize;
        float Widht = Hight * cam.aspect;//aspect => Widht/Hight//비율

        curBound = coll.bounds;

        float minx = curBound.min.x + Widht;
        float miny = curBound.min.y + Hight;

        float maxx = curBound .max.x - Widht;
        float maxy = curBound .max.y - Hight;

        curBound.SetMinMax(new Vector3(minx,miny), new Vector3(maxx,maxy));
    }
}
