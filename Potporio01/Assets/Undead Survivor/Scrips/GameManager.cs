using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //��ũ��Ʈ ���
    Enemy enemy;
    Item item;
    AutoWeaponDron AutoWeaponDron;
    PlayerStatas playerStatas;
    //[Header("������Ʈ ��")]
    ItemManager itemManager;
    //OppositionBullet OpBullet;

    public static GameManager Instance;

    [Header("ī�޶� ��ġ")]
    [SerializeField] Transform camTrs;

    [Header("�÷��̾� ��ġ")]
    [SerializeField] public Transform trsTarget;
     Camera cam;

    [Header("���� ����")]
    [SerializeField] public List<GameObject> mobList;//�� ����Ʈ
    [SerializeField] public List<Transform> mobTrs;//�� ����
    [SerializeField] public Transform CreatTab;
    [SerializeField] List<Vector2> createLine;
    private bool isSpawn = false;
    public bool MobHPUp = false;

    [Header("�� �����ð�")]
    float mobSpawnTimer = 0.0f;// Ÿ�̸�
    [SerializeField, Range(0.1f, 5f)] float mobSpawnTime = 1.0f;

    [Header("������ ����, Ȯ��")]
    [SerializeField] List <GameObject> ItemKind;//������ ����
    [SerializeField] List <GameObject> WeaponKind;//������ ����
    [SerializeField, Range(0.0f, 100f)] float IteamProbability;//�ܺο��� ���� ����
    //bool imItem = false;//������ ���� ����
    [SerializeField] Transform creatItemTab;
    [SerializeField] GameObject creatExp;
    int Esyecount = 0;



    [Header("ü�� �̹���")]
    [SerializeField] HpCanvers hpCanvers;

    [Header("�÷��� �ð�")]
    [SerializeField] TMP_Text timeText;
    float minuteTimer = 0f;
    float minuteTime = 60f;
    float hour = 0f;

    [Header("����")]
    [SerializeField] TMP_Text scoreText;
    int score;

    [Header("���ӿ����޴�")]
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
            Instance = this;//=GameManager�� �ȴ�
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
        //createEnemy();//Ȱ��ȭ �ʿ�
    }


    public void ScorePluse(int number)//���� ����
    {
        score += number;
        scoreText.text = $"{score.ToString("d6")}";
    }

    private void runTime()//�÷��� Ÿ��
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
    /// �÷��̾ �������� �����Ÿ� �ۿ��� �����ϴ� ���
    /// </summary>
    private void createEnemy()
    {
        if (trsTarget == null) { return; }

        mobSpawnTimer += Time.deltaTime;//Ÿ�̸� ON
        if (mobSpawnTimer > mobSpawnTime)//Ÿ�̸Ӱ� ���� ��Ÿ���� �Ѿ��� ���
        {
            int Iroad = Random.Range(0, 4);//������ ��ġ�� ��輱 ����
            //Random.Range(1, 4)1,2,3 

            Vector2 playerPos = trsTarget.position;//player ��ġ
            Vector2 done = new Vector2(0,0); //����Ʈ ����

            float Randamx = Random.Range(-8.0f, 8.0f);//x�� ������ġ ����
            float Randamy = Random.Range(-4.0f, 4.0f);//y�� ������ġ ����

            if (Iroad == 0) //10
            {
                PlusMinas(playerPos.x);//������ ������ ��ȯ
                PlusMinas(playerPos.y);//������ ������ ��ȯ
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
    /// ������ ���� Ȯ�� ����
    /// </summary>
    /// <param name="trs"></param>
    public void CreateItemCheck(Vector3 trs)
    {
        float randam = Random.Range(0f, 100f);
        float WeaponRandam = Random.Range(0f, 100f);
        if ((IteamProbability > randam)== false && 
            IteamProbability > WeaponRandam)//��� ������,����
        {
            if (Esyecount < 2) //�켱 ��� ����
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
            (IteamProbability > randam))//ȸ���� ������
        {
            int count = ItemKind.Count;
            int Number = Random.Range(0, count);

            GameObject go = Instantiate(ItemKind[Number],
                trs, Quaternion.identity, creatItemTab);
            IteamProbability = 0.0f;
        }
        else { IteamProbability += 0.5f; }//Ȯ����
        
    }

    public void rankCheck()
    {
        List<UserData> RankData =
           JsonConvert.DeserializeObject<List<UserData>>
           (PlayerPrefs.GetString(RankIn.rankKey));//������


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
        GameOverMenuScoreText.text = $"���� : {score.ToString("d6")} ";
        if (rank != -1)
        {
            GameOverMenuRankText.text = $"��ŷ : {rank + 1}��";
            IFGameOverMenuRank.gameObject.SetActive(true);
            GameOverMenuBtnText.text = "���";
        }
        else
        {
            GameOverMenuRankText.text = "��ũ�� ���� ���߽��ϴ�";
            IFGameOverMenuRank.gameObject.SetActive(false);
            GameOverMenuBtnText.text = "���θ޴���";
        }

        btnGameOverMenu.onClick.AddListener(() => 
        {
            if (rank != -1)//��ũ�� ����Ҷ�
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
    /// ������ �������� ����
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>����</returns>
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
    /// player�� transform�� ���� ������ ������ ������
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
