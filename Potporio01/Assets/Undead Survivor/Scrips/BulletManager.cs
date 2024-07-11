using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    GameManager gameManager;
    PlayerStatas playerStatas;
    public static BulletManager Instance;
    [SerializeField] Transform playerPos;
    [SerializeField] List<GameObject> bulletList;
    Transform trspos;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;//=GameManager°¡ µÈ´Ù
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        trspos = bulletList[0].transform;
        playerStatas = FindObjectOfType<PlayerStatas>();
    }

    // Update is called once per frame
    void Update()
    {
        //AngleCheck();
    }

    public void AngleCheck()
    {
        float age = 90;
        Vector3 Scale = playerStatas.transform.localScale;
        if (Scale.x == 1)
        {
            age = -Mathf.Abs(age);
        }
        trspos.localRotation = Quaternion.Euler(0, 0, age);
        shoot(age);
    }
    public void shoot(float age)
    {
        //Vector3 pos = transform.localPosition;
        Vector3 Scale = playerStatas.transform.localScale;
        //trspos = bulletList[0].transform;
        if (age == 90)
        {
            //float pos = 1;
            //pos = -Mathf.Abs(pos);
            trspos.transform.Translate(new Vector3(0, -1, 0).normalized * 4.0f * Time.deltaTime);
        }
        else
        {
            //float pos = 1;
            trspos.transform.Translate(new Vector3(0, 1, 0).normalized * 4.0f * Time.deltaTime);
            //trspos.position += new Vector3(-1, 0, 0) * 4.0f * Time.deltaTime;
        }
        //trspos.Translate(new Vector3(0, pos, 0).normalized * 4.0f * Time.deltaTime);
        //trspos.position += new Vector3(pos,0,0) * 4.0f * Time.deltaTime;
    }
}
