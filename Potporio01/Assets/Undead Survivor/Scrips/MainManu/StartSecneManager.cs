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

    [Header("½ÇÇà ¹öÆ°")]
    [SerializeField] Button startBut;
    [SerializeField] Button RankingBut;
    [SerializeField] Button QietBut;
    [SerializeField] Button RankQietBut;
    [SerializeField] GameObject viewRanking;

    [Header("·©Å© ÇÁ¸®ÆÕ")]
    [SerializeField] GameObject fadRank;//·©Å© ÇÁ¸®ÆÕ
    [SerializeField] Transform contents;//»ý¼ºÅÇ
    //Vertex vertex;
    private void Awake()
    {
        RankIn.isStating = true;

        startBut.onClick.AddListener(gameStart);
        RankingBut.onClick.AddListener(showRanking);
        RankQietBut.onClick.AddListener(() => { viewRanking.SetActive(false); });
        QietBut.onClick.AddListener(gameExit);
        initRankView();
        viewRanking.SetActive(false);
    }

    private void initRankView()
    {
        List<UserData> listUserData = null;
        clearRankView();
        if (PlayerPrefs.HasKey(RankIn.rankKey) == true)//·©Å© µ¥ÀÌÅÍ°¡ ÀúÀåÀÌ µÇ¾îÀÖ¾ú´Ù¸é
        {
            listUserData = JsonConvert.DeserializeObject<List<UserData>>
                (PlayerPrefs.GetString(RankIn.rankKey));
        }
        else//·©Å©µ¥ÀÌÅÍ°¡ ÀúÀåµÇ¾î ÀÖÁö ¾Ê¾Ò´Ù¸é
        {
            listUserData = new List<UserData>();
            int rankCount = RankIn.rankCount;
            for (int iNum = 0; iNum < rankCount; ++iNum)
            {
                listUserData.Add(new UserData() { Name = "None", Score = 0 });
            }
            string vale = JsonConvert.SerializeObject(listUserData);
            PlayerPrefs.GetString(RankIn.rankKey, vale);

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
    private void clearRankView()
    {
        int count = contents.childCount;
        for (int iNum = count - 1; iNum > -1; --iNum)
        {
            Destroy(contents.GetChild(iNum).gameObject);
        }
    }
    private void showRanking()
    {
        viewRanking.SetActive(true);
    }

    private void gameExit()
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
        //FaidInOut.Instance.faid = true;
    }


}
