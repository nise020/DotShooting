using Newtonsoft.Json;
using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //��ũ��Ʈ ���
    Enemy enemy;
    Item item;
    AutoWeaponDron AutoWeaponDron;

    public static GameManager Instance;

    [Header("�÷��̾� ��ġ")]
    [SerializeField] public Transform trsTarget;
    [SerializeField] GameObject player;

    [Header("���� ����")]
    [SerializeField] public List<GameObject> mobList;//�� ����Ʈ
    [SerializeField] public List<Transform> mobTrs;//�� ����
    [SerializeField] public Transform CreatTab;//������
    [SerializeField] public List<Vector2> createLine;
    public bool MobstatasUp = false;
    public int PluseHp = 0;
    public float PluseSpeed = 0;
    GameObject mob;
    public int mobSpowncount = 0;
    int mobSpownMaxcount = 8;
    public GameObject[] mobName;

    [Header("�� �����ð�")]
    float mobSpawnTimer = 0.0f;// Ÿ�̸�
    [SerializeField, Range(0.1f, 5f)] float mobSpawnTime = 1.0f;

    [Header("������ ����, Ȯ��")]
    [SerializeField] List <GameObject> itemType;//������ Ÿ��
    [SerializeField] public List <GameObject> ItemKind;//������ ����
    [SerializeField] public List <GameObject> WeaponKind;//������ ����
    [SerializeField] public List <GameObject> buffKind;//������ ����
    [SerializeField, Range(0.0f, 100f)] float IteamProbability;//�ܺο��� ���� ����
    //bool imItem = false;//������ ���� ����
    [SerializeField] Transform creatItemTab;
    public int itemKindNmber;
    public int WeaponKindNmber;
    public int buffKindNmber;
    int priority = 0;

    [Header("��ų ��ư")]
    [SerializeField] Image skillImg;
    [SerializeField] Button skillbtn;
    [SerializeField] Image skillCoolImg;
    float skillCoolTimer = 0f;
    float skillCoolTime = 10f;//ó���� 10��
    bool skillbtnFill = true;
    public bool skillBulletOn = false;
    float skillBulletTimer = 0f;
    float skillBulletTime = 5f;
    float skillCoolBulletTime = 10f;

    [Header("�÷��� �ð�")]
    [SerializeField] TMP_Text timeText;
    public float minuteTimer = 0f;
    float minuteTime = 60f;
    public float hour = 0f;

    [Header("����")]
    [SerializeField] TMP_Text scoreText;
    int score = 0;

    [Header("���ӿ����޴�")]
    [SerializeField] Image backGround;
    [SerializeField] Image textDead;
    public bool deadOn = false;
    public bool colordead = true;
    [SerializeField] GameObject objGameOverMenu;
    [SerializeField] TMP_Text GameOverMenuScoreText;
    [SerializeField] TMP_Text GameOverMenuRankText;
    [SerializeField] TMP_Text GameOverMenuBtnText;
    [SerializeField] TMP_InputField IFGameOverMenuRank;
    [SerializeField] Button btnGameOverMenu;

    [Header("���� ���� �ȳ�")]
    [SerializeField] TMP_Text textStart;
    bool GameStart = false;
    bool Starton = true;

    [Header("�Ͻ�����")]
    [SerializeField] Button StopBtn;
    [SerializeField] GameObject stopImg;
    [SerializeField] Button continuBtn;
    [SerializeField] Button exitBtn;
    [SerializeField] public bool objStop = false;

    [Header("������ ����")]
    [SerializeField] public Image Itemimage;
    [SerializeField] public TMP_Text ItemText;
    Color becolor;
    private void Awake()
    {
        if (RankIn.isStating == false)//�޴����� ���� �ϱ�
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        if (Instance == null)
        {
            Instance = this;//=GameManager�� �ȴ�
        }
        else 
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        textStart.gameObject.SetActive(true);
        Color color = textStart.color;
        color.a = 0f;
        textStart.color = color;
        textDead.color = color;

        becolor = Itemimage.color;
        //ItemText.color;

        skillbtn.onClick.AddListener(skillOn);
        skillbtn.interactable = false;
        skillCoolImg.color = color;
        skillImg.fillAmount = 0;

        stopImg.SetActive(false);
        StopBtn.onClick.AddListener(StopBtnOn);
        continuBtn.onClick.AddListener(continuBtnOn);
        exitBtn.onClick.AddListener(exitGame);
    }
    public void Tooltip(string name) 
    {
        if (name == "Sword") 
        {
            PlayerStatas playerStatas = player.GetComponent<PlayerStatas>();
            if (playerStatas.DropSword == false) 
            {
                ItemText.text = $"Auto Sword�� ȹ���ϼ̽��ϴ�";
            }
            else { ItemText.text = $"�̹� ȹ���ϼ̽��ϴ�"; }
            
        }
        else if (name == "OpGun")
        {
            PlayerStatas playerStatas = player.GetComponent<PlayerStatas>();
            if (playerStatas.DropOpGun == false)
            {
                ItemText.text = $"Random Target Gun�� ȹ���ϼ̽��ϴ�";
            }
            else { ItemText.text = $"�̹� ȹ���ϼ̽��ϴ�"; }
             
        }
        else if (name == "BoundGun")
        {
            PlayerStatas playerStatas = player.GetComponent<PlayerStatas>();
            if (playerStatas.DropBoundGun == false)
            {
                ItemText.text = $"Bound Gun�� ȹ���ϼ̽��ϴ�";
            }
            else { ItemText.text = $"�̹� ȹ���ϼ̽��ϴ�"; }
             
        }
        else if (name == "MaxHpUp") { ItemText.text = $"�ִ� ü���� ���� �Ͽ����ϴ�"; }
        else if (name == "Heal") 
        {
            PlayerStatas playerStatas  = player.GetComponent<PlayerStatas>();
            if (playerStatas.NowHp <= playerStatas.MaximumHP)
            {
                ItemText.text = $"ü���� ȸ�� �Ǿ����ϴ�({playerStatas.NowHp}/{playerStatas.MaximumHP})";
            }
            else { ItemText.text = "�� �̻� ü�� ȸ���� �Ұ��մϴ�"; }
        }
        else if (name == "SpeedUp") 
        {
            MoveControll moveControll = player.GetComponent<MoveControll>();
            if (moveControll.moveSpeed < moveControll.MaxiumSpeed) 
            {
                ItemText.text = $"�̵��ӵ��� ���� �Ͽ����ϴ�({moveControll.moveSpeed}/{moveControll.MaxiumSpeed})"; 
            }
            else { ItemText.text = "�̵��ӵ��� �ִ�ġ�� �����߽��ϴ�"; }
        }
        else if (name == "SwordPluse") 
        {
            AttakProces attakProces = player.GetComponent<AttakProces>();
            if (attakProces.swordPlusecount < attakProces.swordPluseMaxcount)
            {
                ItemText.text = $"���� ������ ���� �Ͽ����ϴ�({attakProces.swordPlusecount}/{attakProces.swordPluseMaxcount})";
            }
            else { ItemText.text = "���� ������ �ִ�ġ�� �����߽��ϴ�"; } 
        }
        else if (name == "SwordUgraid") 
        {
            AttakProces attakProces = player.GetComponent<AttakProces>();
            if (attakProces.swordUpgraidcount < attakProces.swordUpgraidMaxcount)
            {
                ItemText.text = $"���� Level�� ���� �Ͽ����ϴ�({attakProces.swordUpgraidcount}/{attakProces.swordUpgraidMaxcount})";
            }
            else { ItemText.text = "Level�� �ִ�ġ�� �����߽��ϴ�"; }
        }
        else if (name == "SwordScaleUP")
        {
            AttakProces attakProces = player.GetComponent<AttakProces>();
            if (attakProces.swordScalecount < attakProces.swordScaleMaxcount)
            {
                ItemText.text = $"���� ũ�Ⱑ ���� �Ͽ����ϴ�({attakProces.swordScalecount}/{attakProces.swordScaleMaxcount})";
            }
            else { ItemText.text = "���� ũ�Ⱑ �ִ�ġ�� �����߽��ϴ�"; }
        }
        Itemimage.color = becolor;
        ItemText.color = becolor;

        Itemimage.gameObject.SetActive(true);
        ItemText.gameObject.SetActive(true);

    }
    private void TooltipColor() 
    {
        if (ItemText.gameObject.activeSelf) 
        {
            Color color2 = ItemText.color;
            color2.a -= Time.deltaTime / 3f;
            ItemText.color = color2;
            if (ItemText.color.a < 0)
            {
                ItemText.gameObject.SetActive(false);
            }
        }
        else
        {
            Color color1 = Itemimage.color;
            color1.a -= Time.deltaTime / 2f;
            Itemimage.color = color1;
            if (Itemimage.color.a < 0)
            {
                Itemimage.gameObject.SetActive(false);
            }
        }
        

    }

    private void ItemTextTime() 
    {
        if (Itemimage.gameObject.activeSelf)//������Ʈ�� ���� ������
        {
            TooltipColor();
        }
    }
    private void continuBtnOn()
    {
        stopImg.SetActive(false);
        objStop = false;
    }
    private void StopBtnOn() 
    {
        stopImg.SetActive(true);
        objStop = true; 
    }
    private void exitGame() 
    {
        stopImg.SetActive(false);
        objStop = false;
        FaidInOut.Instance.ActiveFade(true, () =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            FaidInOut.Instance.ActiveFade(false, null);
        });

    }
    private void Update()
    {
        dead(deadOn);
        if (objStop == true) { return; }
        gameStart();
        if (GameStart == false) {return; }
        runTime();
        skiilCool();
        createEnemy();//Ȱ��ȭ �ʿ�
        ItemTextTime();

    }
    private void skillOn() 
    {
        skillbtn.interactable = false;
        skillBulletOn = true;
    }
    private void SkillCoolStart()//��ų �������϶� �÷� 
    {
        Color color = skillCoolImg.color;
        color.a -= Time.deltaTime / skillBulletTime; ;
        if (color.a < 0)
        {
           color.a = 0;
        }
        skillCoolImg.color = color;
        if (skillCoolImg.color.a <= 0) { return; }
    }
    private void SkillCoolRuning()//�ð������� ���̴� ��Ÿ��
    {
        Color color = skillCoolImg.color;
        color.a += Time.deltaTime / skillCoolBulletTime;
        if (color.a > 1)
        {
            color.a = 1;
        }
        skillCoolImg.color = color;
        if (skillCoolImg.color.a > 1) { return; }
    }

    private void skiilCool()//��ų �� Ÿ�� ����
    {
        if (skillbtn.interactable == true || trsTarget == null || skillImg == null) { return; }

        if (skillbtn.interactable == false)//Ÿ�̸� �� �����ʿ�
        {
            skillBulletTimer += Time.deltaTime;
            if (skillBulletTimer > skillBulletTime)//��ų ���ӽð�
            {
                skillBulletOn = false;//��ų��ư
            }

            if (skillBulletOn == true)//��ų On
            {
                SkillCoolStart();
                skillImg.fillAmount -= Time.deltaTime / skillBulletTime;
            }
            else//skillBulletOn == false,��ų Off
            {
                SkillCoolRuning();
                skillImg.fillAmount += Time.deltaTime / skillCoolBulletTime;
            }

            skillCoolTimer += Time.deltaTime;
            if (skillCoolTimer > skillCoolTime && skillCoolImg.color.a <= 1f)//��ų ��Ÿ��
            {
                skillCoolTime = 15f;//���Ŀ��� 15�� ����
                skillCoolTimer = 0f;
                skillbtn.interactable = true;
                skillBulletTimer = 0f;
            }
            
        }
        

    }

    private void gameStart()//���� ����
    {
        if (textStart == null) { return; }
        if (Starton == true)
        {
            Color color = textStart.color;
            color.a += Time.deltaTime/ 1.0f;
            textStart.color = color;
            if (color.a > 1)
            {
                color.a = 1f;
                Starton = false;
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
    private void dead(bool value) 
    {
        if (value == false || textDead == null) { return; }
        backGround.gameObject.SetActive(true);
        if (colordead == true)
        {
            Color color = textDead.color;
            color.a += Time.deltaTime / 1.0f;
            textDead.color = color;
            if (color.a > 1)
            {
                color.a = 1f;
                colordead = false;
            }
        }
        else
        {
            Color color = textDead.color;
            color.a -= Time.deltaTime / 1.0f;
            textDead.color = color;
            if (color.a < 0)
            {
                color.a = 0f;
            }
        }
        if (textDead.color.a <= 0)
        {
            Destroy(textDead.gameObject);
            rankCheck();
            value = true;
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
            PluseSpeed += 0.5f;
            mobSpawnTime -= 0.3f;
            mobSpownMaxcount += 5;
        }
    }

    /// <summary>
    /// �÷��̾ �������� �����Ÿ� �ۿ��� �����ϴ� ���
    /// </summary>
    private void createEnemy()
    {
        if (trsTarget == null || mobSpowncount >= mobSpownMaxcount)//Ÿ�̸Ӱ� ���� ��Ÿ���� �Ѿ��� ���
        { return; }

        mobSpawnTimer += Time.deltaTime;//Ÿ�̸� ON
        if (mobSpawnTimer > mobSpawnTime && mobSpowncount <= mobSpownMaxcount)//Ÿ�̸Ӱ� ���� ��Ÿ���� �Ѿ��� ���
        {
            mobSpawnTimer = 0.0f;
            mobSpowncount += 1;
            int Iroad = Random.Range(0, 4);//������ ��ġ�� ��輱 ����
            //Random.Range(1, 4)1,2,3 

            Vector2 playerPos = trsTarget.position;//player ��ġ
            Vector2 done = new Vector2(0, 0); //����Ʈ ����

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
            int randamKind = Random.Range(0, 2);
            if (randamKind == 0f)
            {
                CreateItem(trs, ItemKind, out itemKindNmber);
            }
            else if (randamKind == 1)
            {
                if (WeaponKind.Count != 0)//�켱�� ����
                {
                    CreateItem(trs, WeaponKind, out WeaponKindNmber);
                }
                else 
                {
                    if (buffKind.Count != 0)
                    {
                        CreateItem(trs, buffKind, out buffKindNmber);
                    }
                    else { return; }
                }  
            }           
        }
        else //Ȯ����
        {
            IteamProbability += 0.5f;
        }

    }

    /// <summary>
    /// ������ ����
    /// </summary>
    /// <param name="��ġ"></param>
    /// <param name="������Ʈ List"></param>
    /// <param name="value"></param>
    private void CreateItem(Vector3 trs, List<GameObject> obj, out int value) 
    {
        int count = obj.Count;
        int Number = Random.Range(0, count);

        GameObject go = Instantiate(obj[Number],
            trs, Quaternion.identity, creatItemTab);
        IteamProbability = 0.0f;//Ȯ��
        value = Number;//�������� ���� ���
    }

    public void rankCheck()
    {
        List<UserData> userData =
           JsonConvert.DeserializeObject<List<UserData>>
           (PlayerPrefs.GetString(RankIn.rankKey));

        int rank = -1;
        int count = userData.Count;
        for (int iNum = 0; iNum < count; iNum++) 
        {
            UserData userDate = userData[iNum];
            if (userDate.Score < score) 
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

                userData.Insert(rank, newRank);
                userData.RemoveAt(userData.Count - 1);
                string value = JsonConvert.SerializeObject(userData);
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
        _pos = trsTarget.transform.position;
    }


}
