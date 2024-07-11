using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    //OppositionBullet OpBullet;

    public static GameManager Instance;

    [Header("카메라 위치")]
    [SerializeField] Transform camTrs;

    [Header("플레이어 위치")]
    [SerializeField] public Transform trsTarget;
     Camera cam;

    [Header("몬스터 생성")]
    [SerializeField] public List<GameObject> mobList;//몹 리스트
    [SerializeField] public List<Transform> mobTrs;//몹 상태
    [SerializeField] public Transform CreatTab;
    [SerializeField] List<Vector2> createLine;
    private bool isSpawn = false;
    public bool MobHPUp = false;

    [Header("적 생성시간")]
    float mobSpawnTimer = 0.0f;// 타이머
    [SerializeField, Range(0.1f, 5f)] float mobSpawnTime = 1.0f;

    [Header("아이템 종류, 확률")]
    [SerializeField] List <GameObject> ItemKind;//아이템 종류
    [SerializeField] List <GameObject> WeaponKind;//아이템 종류
    [SerializeField, Range(0.0f, 100f)] float IteamProbability;//외부에서 조정 가능
    //bool imItem = false;//아이템 생성 여부
    [SerializeField] Transform creatItemTab;
    [SerializeField] GameObject creatExp;
    int Esyecount = 0;



    [Header("체력 이미지")]
    [SerializeField] HpCanvers hpCanvers;

    [Header("플레이 시간")]
    [SerializeField] TMP_Text timeText;
    float minuteTimer = 0f;
    float minuteTime = 60f;
    float hour = 0f;

    [Header("점수")]
    [SerializeField] TMP_Text scoreText;
    int score;

    [Header("게임오버메뉴")]
    [SerializeField] GameObject objGameOverMenu;
    [SerializeField] TMP_Text GameOverMenuScoreText;
    [SerializeField] TMP_Text GameOverMenuRankText;
    [SerializeField] TMP_Text GameOverMenuBtnText;
    [SerializeField] TMP_InputField IFGameOverMenuRank;
    [SerializeField] Button btnGameOverMenu;


    private void Awake()
    {
        if (RankIn.isStating == false)
        {
            //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        if (Instance == null)
        {
            Instance = this;//=GameManager가 된다
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
        //cam = Camera.main;
    }

    private void Update()
    {
        runTime();
        //createEnemy();//활성화 필요
    }


    public void ScorePluse(int number)//점수 증가
    {
        score += number;
        scoreText.text = $"{score.ToString("d6")}";
    }

    private void runTime()//플레이 타임
    {
        minuteTimer += 1 * Time.deltaTime;
        //int count = minuteTimer;
        timeText.text = $"{((int)hour).ToString("d2")}:{((int)minuteTimer).ToString("d2")}";
        if (minuteTimer> minuteTime) 
        {
            minuteTimer = 0;
            hour += 1;
            mobSpawnTime -= 0.2f;
            MobHPUp = true;
        }

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


        }

    }


    /// <summary>
    /// 아이템 생성 확률 계산기
    /// </summary>
    /// <param name="trs"></param>
    public void CreateItemCheck(Vector3 trs)
    {
        float randam = Random.Range(0f, 100f);
        float WeaponRandam = Random.Range(0f, 100f);
        if ((IteamProbability > randam)== false && 
            IteamProbability > WeaponRandam)//장비 아이템,버프
        {
            if (Esyecount < 2) //우선 드랍 설정
            {
                int number = Random.Range(3, 5);

                GameObject Go = Instantiate(WeaponKind[number],
                    trs, Quaternion.identity, creatItemTab);
                Esyecount += 1;
                WeaponKind[number] = null;
                return;
            }
            int count = WeaponKind.Count;
            int Number = Random.Range(0, count);

            GameObject go = Instantiate(WeaponKind[Number],
                trs, Quaternion.identity, creatItemTab);
            IteamProbability = 0.0f;

        }     
        else if((IteamProbability > WeaponRandam) == false && 
            (IteamProbability > randam))//회복용 아이템
        {
            int count = ItemKind.Count;
            int Number = Random.Range(0, count);

            GameObject go = Instantiate(ItemKind[Number],
                trs, Quaternion.identity, creatItemTab);
            IteamProbability = 0.0f;
        }
        else { IteamProbability += 0.5f; }//확률업
        
    }

    public void rankCheck()
    {
        List<UserData> RankData =
           JsonConvert.DeserializeObject<List<UserData>>
           (PlayerPrefs.GetString(RankIn.rankKey));//에러뜸


        Debug.Log(RankIn.rankKey);
        Debug.Log(PlayerPrefs.GetString(RankIn.rankKey));
        int rank = -1;
        int count = RankData.Count;
        for (int iNum = 0; iNum < count; iNum++) 
        {
            UserData userDate = RankData[iNum];
            if (userDate.Score<score) 
            {
                rank = iNum;
                break;
            }
        }
        GameOverMenuScoreText.text = $"점수 : {score.ToString("d6")} ";
        if (rank != -1)
        {
            GameOverMenuRankText.text = $"랭킹 : {rank + 1}등";
            IFGameOverMenuRank.gameObject.SetActive(true);
            GameOverMenuBtnText.text = "등록";
        }
        else
        {
            GameOverMenuRankText.text = "랭크인 하지 못했습니다";
            IFGameOverMenuRank.gameObject.SetActive(false);
            GameOverMenuBtnText.text = "메인메뉴로";
        }

        btnGameOverMenu.onClick.AddListener(() => 
        {
            if (rank != -1)//랭크에 등록할때
            {
                string name = IFGameOverMenuRank.text;
                if (name == string.Empty) 
                {
                    name = "aaa";
                }
                UserData newRank = new UserData();
                newRank.Score = score;
                newRank.Name = name;

                RankData.Insert(rank, newRank);
                RankData.RemoveAt(RankData.Count - 1);

                string value = JsonConvert.SerializeObject(RankData);
                PlayerPrefs.SetString((RankIn.rankKey), value);
            }

            FaidInOut.Instance.faid = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        });
        objGameOverMenu.SetActive(true);
    }

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
        _pos = trsTarget.position;
    }

    public void EnomyTrsPosiTion(out Vector3 _pos)
    {
        _pos = mobList[1].transform.position;
    }


}
