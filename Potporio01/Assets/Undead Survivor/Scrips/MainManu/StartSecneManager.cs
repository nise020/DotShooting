using Newtonsoft.Json;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSecneManager : MonoBehaviour
{
    [SerializeField] Button Pushbut;
    [SerializeField] Image butImage;
    [SerializeField] TMP_Text startText;
    float runningTimer = 0f;
    float timer = 1.0f;
    [SerializeField] bool on = false;

    [Header("실행 버튼")]
    [SerializeField] Button startBut;
    [SerializeField] Button RankingBut;
    [SerializeField] Button QietBut;
    [SerializeField] Button RankQietBut;
    [SerializeField] GameObject viewRanking;
    [SerializeField] GameObject Fadeobj;

    [Header("랭크 프리팹")]
    [SerializeField] GameObject fadRank;//랭크 프리팹
    [SerializeField] GameObject TitleName;//제목
    [SerializeField] Transform contents;//생성탭
    //Vertex vertex;
    private void Awake()
    {
        RankIn.isStating = true;
        Fadeobj.SetActive(true);
        startBut.onClick.AddListener(gameStart);
        RankingBut.onClick.AddListener(showRanking);
        RankQietBut.onClick.AddListener(reTurnManu);
        QietBut.onClick.AddListener(gameExit);
        initRankView();
        viewRanking.SetActive(false);
    }
    private void reTurnManu() 
    {
        viewRanking.SetActive(false);
        TitleName.SetActive(true);

    }
    private void initRankView()
    {
        List<UserData> listUserData = null;
        clearRankView();
        if (PlayerPrefs.HasKey(RankIn.rankKey) == true)//랭킹 데이터가 저장이 되어있었다면
        {
            listUserData = JsonConvert.DeserializeObject<List<UserData>>
                (PlayerPrefs.GetString(RankIn.rankKey));
        }
        else//랭킹 데이터가 없을때
        {
            listUserData = new List<UserData>();
            int rankCount = RankIn.rankCount;
            for (int iNum = 0; iNum < rankCount; ++iNum)
            {
                listUserData.Add(new UserData() { Name = "None", Score = 0 });
            }
            string vale = JsonConvert.SerializeObject(listUserData);
            PlayerPrefs.SetString(RankIn.rankKey, vale);

        }


        int count = listUserData.Count; 
        for (int iNum = 0; iNum < count; ++iNum)
        {
            UserData data = listUserData[iNum];

            GameObject go = Instantiate(fadRank, contents);
            FabRanking goSc = go.GetComponent<FabRanking>();
            goSc.SetData((iNum + 1).ToString(), data.Name, data.Score);
        }
        
    }
    private void clearRankView()//랭크를 항상 10개 유지
    {
        int count = contents.childCount;
        for (int iNum = count - 1; iNum > -1; --iNum)
        {
            Destroy(contents.GetChild(iNum).gameObject);
        }
    }
    private void showRanking()
    {
        TitleName.SetActive(false);
        viewRanking.SetActive(true);
    }

    private void gameExit()//게임씬에서 종료
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }

    private void gameStart()
    {
        FaidInOut.Instance.ActiveFade(true, ()=>
        {
            SceneManager.LoadScene(1);
            FaidInOut.Instance.ActiveFade(false, null);
        });
    }


}
