using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //스크립트 등록
    Enemy enemy;
    Item item;
    AutoWeaponDron AutoWeaponDron;
    PlayerStatas playerStatas;
    //[Header("오브젝트 들")]
    ItemManager itemManager;
    

    public static GameManager Instance;

    [Header("카메라 위치")]
    [SerializeField] Transform camTrs;

    [Header("플레이어 위치")]
    [SerializeField] public Transform trsTarget;
     Camera cam;

    [Header("몬스터 생성")]
    [SerializeField] public List<GameObject> mobList;//몹 리스트
    [SerializeField] public List<Transform> mobTrs;//몹 상태
    [SerializeField] Transform CreatTab;
    [SerializeField] List<Vector2> createLine;
    private bool isSpawn = false;

    [Header("적 생성시간")]
    float mobSpawnTimer = 0.0f;// 타이머
    [SerializeField, Range(0.1f, 5f)] float mobSpawnTime = 1.0f;

    [Header("아이템 종류, 확률")]
    [SerializeField] List <GameObject> ItemKind;//아이템 종류
    [SerializeField, Range(0.0f, 100f)] float IteamProbability;//외부에서 조정 가능
    //bool imItem = false;//아이템 생성 여부
    [SerializeField] Transform creatItemTab;
    [SerializeField] GameObject creatExp;

    [Header("유도 총알")]
    [SerializeField] Transform bulletTrnspos;

    [Header("아이템 확률")]
    [SerializeField] List<GameObject> bulletObj;

    [Header("체력 이미지")]
    [SerializeField] HpCanvers hpCanvers;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;//=GameManager가 된다
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
        cam = Camera.main;
    }
    private void Update()
    {
        createEnemy();//활성화 필요
    }

    /// <summary>
    /// 플레이어가 움직여도 일정거리 밖에서 스폰하는 기능
    /// </summary>
    private void createEnemy()
    {
        if (trsTarget == null) { return; }

        mobSpawnTimer += Time.deltaTime;//타이머 ON
        if (mobSpawnTimer > mobSpawnTime)//타이머가 스폰 쿨타임을 넘었을 경우
        {
            int Iroad = Random.Range(0, 4);//스폰할 위치의 경계선 선정
            //Random.Range(1, 4)1,2,3 

            Vector2 playerPos = trsTarget.position;//player 위치
            Vector2 done = new Vector2(0,0); //디폴트 생성

            float Randamx = Random.Range(-8.0f, 8.0f);//x값 랜덤위치 스폰
            float Randamy = Random.Range(-4.0f, 4.0f);//y값 랜덤위치 스폰

            if (Iroad == 0) //10
            {
                PlusMinas(playerPos.x);//음수를 정수로 전환
                PlusMinas(playerPos.y);//음수를 정수로 전환
                done = new Vector2(createLine[0].x + playerPos.x, Randamy + playerPos.y);
            }
            else if (Iroad == 1)//-10
            {
                PlusMinas(playerPos.x);
                PlusMinas(playerPos.y);
                done = new Vector2(createLine[1].x + playerPos.x, Randamy + playerPos.y);
            }
            else if (Iroad == 2)//7
            {
                PlusMinas(playerPos.x);
                PlusMinas(playerPos.y);
                done = new Vector2(Randamx + playerPos.x, createLine[2].y + playerPos.y);
            }
            else//Iroad == 3//-7
            {
                PlusMinas(playerPos.x);
                PlusMinas(playerPos.y);
                done = new Vector2(Randamx + playerPos.x, createLine[3].y + playerPos.y);
            }
            //done.x = Mathf.Clamp(done.x, -10, 10);
            //done.y = Mathf.Clamp(done.y, -7, 7);

            int mobcount = mobList.Count;
            int mobiRoad = Random.Range(0, mobcount);

            GameObject go = Instantiate(mobList[mobiRoad], done, Quaternion.identity, CreatTab);
            mobSpawnTimer = 0.0f;
            #region 일일히 예외처리 노가다
            //int count = 1;
            //for (int INum = 0; INum < count; INum++)
            //{
            //    if (Iroad == 1)//x=10
            //    {
            //        if (defoltPos.x < 0.0f && defoltPos.y < 0.0f)// -/-
            //        {
            //            defoltPos.x *= defoltPos.x + createLine[0].x;
            //            defoltPos.y *= defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y < 0.0f)//+/-
            //        {
            //            defoltPos.x = defoltPos.x + createLine[0].x;
            //            defoltPos.y *= defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y > 0.0f)//+/+
            //        {
            //            defoltPos.x = defoltPos.x + createLine[0].x;
            //            defoltPos.y = defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }
            //        else if (defoltPos.x < 0.0f && defoltPos.y > 0.0f)//-/+
            //        {
            //            defoltPos.x *= defoltPos.x + createLine[0].x;
            //            defoltPos.y = defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }

            //    }
            //    else if (Iroad == 2)//x=-10
            //    {
            //        if (defoltPos.x < 0.0f && defoltPos.y < 0.0f)// -/-
            //        {
            //            defoltPos.x = defoltPos.x + createLine[1].x;
            //            defoltPos.y = defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y < 0.0f)//+/-
            //        {
            //            defoltPos.x *= defoltPos.x + createLine[1].x;
            //            defoltPos.y = defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y > 0.0f)//+/+
            //        {
            //            defoltPos.x *= defoltPos.x + createLine[1].x;
            //            defoltPos.y *= defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }
            //        else if (defoltPos.x < 0.0f && defoltPos.y > 0.0f)//-/+
            //        {
            //            defoltPos.x = defoltPos.x + createLine[1].x;
            //            defoltPos.y += defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }
            //    }
            //    else if (Iroad == 3)//y=7
            //    {
            //        if (defoltPos.x < 0.0f && defoltPos.y < 0.0f)// -/-
            //        {
            //            defoltPos.y *= defoltPos.y + createLine[2].y;
            //            defoltPos.x *= defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y < 0.0f)//+/-
            //        {
            //            defoltPos.y = defoltPos.y + createLine[2].y;
            //            defoltPos.x *= defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y > 0.0f)//+/+
            //        {
            //            defoltPos.y = defoltPos.y + createLine[2].y;
            //            defoltPos.x = defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }
            //        else if (defoltPos.x < 0.0f && defoltPos.y > 0.0f)//-/+
            //        {
            //            defoltPos.y *= defoltPos.y + createLine[2].y;
            //            defoltPos.x = defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }

            //    }
            //    else if (Iroad == 4)//y=7
            //    {
            //        if (defoltPos.x < 0.0f && defoltPos.y < 0.0f)// -/-
            //        {
            //            defoltPos.y = defoltPos.y + createLine[2].y;
            //            defoltPos.x = defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y < 0.0f)//+/-
            //        {
            //            defoltPos.y *= defoltPos.y + createLine[2].y;
            //            defoltPos.x = defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y > 0.0f)//+/+
            //        {
            //            defoltPos.y *= defoltPos.y + createLine[2].y;
            //            defoltPos.x *= defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }
            //        else if (defoltPos.x < 0.0f && defoltPos.y > 0.0f)//-/+
            //        {
            //            defoltPos.y = defoltPos.y + createLine[2].y;
            //            defoltPos.x *= defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }
            //    }
            //    done = defoltPos;
            //    if ((done.x > 10 && done.x > -10) ||
            //        (done.y > 7 && done.y > -7))
            //    {
            //        //y=4//x=8
            //        defoltPos = targetTransform.position;
            //        INum = 0;
            //        continue;
            //    }
            //    done = defoltPos;
            //}
            #endregion

            #region 끄적이다 주석처리한거
            //GameObject go = Instantiate(mobList[mobiRoad], defoltPos,Quaternion.identity, CreatTab);
            // GameObject poroses01 = Instantiate(mobList[mobiRoad], defoltPos,
            // Quaternion.identity, CreatTab);
            //GameObject poroses02 = Instantiate(mobList[mobiRoad], CreatTab);
            //Enemy gosc = go.GetComponent<Enemy>();
            //CameraPosition,
            #endregion
        }

    }


    /// <summary>
    /// 아이템 생성 확률 계산기
    /// </summary>
    /// <param name="trs"></param>
    public void CreateItemCheck(Vector3 trs)//경험치 아이템 고민중
    {
        float randam = Random.Range(0f, 100f);
        if (IteamProbability >= randam)//확률 부분
        {
            int count = ItemKind.Count;
            int Number = Random.Range(0, count);

            GameObject go = Instantiate(ItemKind[Number],
                trs, Quaternion.identity, creatItemTab);
            IteamProbability = 0.0f;
        }
        else //경험치 생성 코드위치
        {
            //GameObject go = Instantiate( creatExp,
            //    trs, Quaternion.identity, creatItemTab);
            IteamProbability += 0.5f;
        }
    }
    #region Hpcheck주석
    //public void Hpcheck(float now, float max)
    //{
    //    hpCanvers.HpBar(now, max);
    //}
    #endregion

    /// <summary>
    /// 정수를 내보내는 계산기
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>정수</returns>
    public float PlusMinas(float pos)
    {
        if (pos > 0) 
        {
            return pos;
        }
        else if (pos < 0) 
        {
            pos = Mathf.Abs(pos);
        }
        return pos;
    }

    /// <summary>
    /// player의 transform에 대한 정보를 밖으로 꺼낸다
    /// </summary>
    /// <param name="_pos"></param>
    public void PlayerTrsPosiTion(out Vector3 _pos)
    {
        _pos = trsTarget.localPosition;
    }




}
