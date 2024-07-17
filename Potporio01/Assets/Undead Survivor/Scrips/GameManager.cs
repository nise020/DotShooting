using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    //OppositionBullet OpBullet;

    public static GameManager Instance;

    [Header("�÷��̾� ��ġ")]
    [SerializeField] public Transform trsTarget;

    [Header("���� ����")]
    [SerializeField] public List<GameObject> mobList;//�� ����Ʈ
    [SerializeField] public List<Transform> mobTrs;//�� ����
    [SerializeField] public Transform CreatTab;//������
    [SerializeField] List<Vector2> createLine;
    public bool MobstatasUp = false;
    public int PluseHp = 0;
    GameObject mob;

    [Header("�� �����ð�")]
    float mobSpawnTimer = 0.0f;// Ÿ�̸�
    [SerializeField, Range(0.1f, 5f)] float mobSpawnTime = 1.0f;

    [Header("������ ����, Ȯ��")]
    [SerializeField] List <GameObject> ItemKind;//������ ����
    [SerializeField] List <GameObject> WeaponKind;//������ ����
    [SerializeField] List <GameObject> buffKind;//������ ����
    [SerializeField, Range(0.0f, 100f)] float IteamProbability;//�ܺο��� ���� ����
    //bool imItem = false;//������ ���� ����
    [SerializeField] Transform creatItemTab;
    int priority = 0;

    [Header("��ų ��ư")]
    [SerializeField] Button skillbtn;
    [SerializeField] Image skillCoolImg;
    float skillCoolTimer = 0f;
    float skillCoolTime = 5f;
    bool skillbtnFill = true;
    public bool skillBulletOn = false;
    float skillBulletTimer = 0f;

    [Header("�÷��� �ð�")]
    [SerializeField] TMP_Text timeText;
    public float minuteTimer = 0f;
    float minuteTime = 60f;
    public float hour = 0f;

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

    [Header("���� ���� �ȳ�")]
    [SerializeField] TMP_Text textStart;
    bool GameStart = false;
    bool on = true;

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
    }
    private void Start()
    {
        playerStatas = FindObjectOfType<PlayerStatas>();
        Color color = textStart.color;
        color.a = 0f;
        textStart.color = color;
        skillbtn.onClick.AddListener(() => { skillbtn.interactable = false; });
        skillbtn.interactable = false;
        SkillCoolStart();
    }
    private void Update()
    {
        gameStart();
        if (GameStart == false) {return; }
        runTime();
        skiilCool();
        //createEnemy();//Ȱ��ȭ �ʿ�
        
    }
    private void SkillCoolStart() 
    {
        Color color = skillCoolImg.color;
        color.a = 0f;
        skillCoolImg.color = color;
    }
    private void SkillCoolRuning()
    {
        Color color = skillCoolImg.color;
        color.a += Time.deltaTime / skillCoolTime;
        if (color.a > 1)
        {
            color.a = 1;
        }
        skillCoolImg.color = color;
    }
    private void skiilCool()
    {
        if (skillbtn.interactable == true) { return; }
        if (skillbtn.interactable == false)//Ÿ�̸� �� �����ʿ�
        {
            if (skillbtnFill == true) 
            {
                SkillCoolStart();
                skillbtnFill = false;
            }
            skillBulletOn = true;//5�� ���� ����
            skillBulletTimer += Time.deltaTime;
            if (skillBulletTimer > 5.0f) 
            {
                skillBulletOn = false;
                skillBulletTimer = 0f;
            }
            SkillCoolRuning();

            skillCoolTimer += Time.deltaTime;
            if (skillCoolTimer > skillCoolTime && skillCoolImg.color.a <= 1f)
            {
                skillCoolTimer = 0f;
                skillbtn.interactable = true;
                skillbtnFill = true;
            }

        }
        
        
    }

    private void gameStart()
    {
        if (textStart==null) { return; }
        if (on == true)
        {
            Color color = textStart.color;
            color.a += Time.deltaTime/ 1.0f;
            textStart.color = color;
            if (color.a > 1)
            {
                color.a = 1f;
                on = false;
            }
        }
        else
        {
            Color color = textStart.color;
            color.a -= Time.deltaTime / 1.0f;
            textStart.color = color;
            if (color.a < 0)
            {
                color.a = 0f;
                Destroy(textStart.gameObject);
            }
        }
        if (textStart.color.a <= 0) 
        {
            Destroy(textStart.gameObject);
            GameStart = true;
        }
        
    }

    public void ScorePluse(int number)//���� ����
    {
        score += number;
        scoreText.text = $"{score.ToString("d6")}";
    }

    private void runTime()//�÷��� Ÿ��
    {
        timeText.text = $"{((int)hour).ToString("d2")}:{((int)minuteTimer).ToString("d2")}";

        if (trsTarget == null) { return; }
        minuteTimer += 1 * Time.deltaTime;

        if (minuteTimer> minuteTime) 
        {
            minuteTimer = 0;
            hour += 1;
            PluseHp += 1;
            mobSpawnTime -= 0.2f;
            
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

            mob = Instantiate(mobList[mobiRoad], done, Quaternion.identity, CreatTab);
            Enemy enemy = mob.GetComponent<Enemy>();
            enemy.pluse();
            mobSpawnTimer = 0.0f;

        }

    }


    /// <summary>
    /// ������ ���� Ȯ�� ����
    /// </summary>
    /// <param name="trs"></param>
    public void CreateItemProbability(Vector3 trs)
    {
        float randam = Random.Range(0f, 100f);
        if (IteamProbability > randam) 
        {
            float randamKind = Random.Range(0f, 2f);
            if (randamKind == 0) 
            {
                CreateItem(trs, ItemKind, out int value);
                IteamProbability = 0.0f;
            }
            else if (randamKind > 0)
            {
                if (priority < 2)//�켱�� ����
                {
                    CreateItem(trs, WeaponKind, out int number);
                    priority += 1;
                    WeaponKind.RemoveAt(number);//�����
                    return;
                }

                CreateItem(trs, buffKind, out int value);
                IteamProbability = 0.0f;
            }
            else //Ȯ����
            {
                if (WeaponKind == null) { IteamProbability += 0.5f; }
                else { IteamProbability += 2f; } 
            }
        }

        
    }
    private void CreateItem(Vector3 trs, List<GameObject> obj, out int value) 
    {
        int count = obj.Count;
        int Number = Random.Range(0, count);

        GameObject go = Instantiate(WeaponKind[Number],
            trs, Quaternion.identity, creatItemTab);
        IteamProbability = 0.0f;
        value = Number;
    }
    public void rankCheck()
    {
        List<UserData> listUserData =
           JsonConvert.DeserializeObject<List<UserData>>
           (PlayerPrefs.GetString(RankIn.rankKey));


        int rank = -1;
        if(listUserData.Count < RankIn.rankCount) 
        {
            while (listUserData.Count < RankIn.rankCount)//�ܼ� �ں���
            {
                listUserData.Add(new UserData() { Name = "None", Score = 0 });

            }
        }
        int count = listUserData.Count;
        
        for (int iNum = 0; iNum < count; iNum++) 
        {
            UserData userDate = listUserData[iNum];
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

                listUserData.Insert(rank, newRank);
                listUserData.RemoveAt(listUserData.Count - 1);
                string value = JsonConvert.SerializeObject(listUserData);
                PlayerPrefs.SetString((RankIn.rankKey), value);
            }

            FaidInOut.Instance.ActiveFade(true, () =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                FaidInOut.Instance.ActiveFade(false, null);
            });
            
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
