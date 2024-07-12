using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManu : MonoBehaviour
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
    //Vertex vertex;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        startRunig();
        startBut.onClick.AddListener(gameStart);
    }

    private void gameStart()
    {
        SceneManager.LoadScene(1);
        FaidInOut.Instance.faid = true;
    }

    private void startRunig()//스타트 버튼 깜빡임
    {
        //if (startBut.onClick == true) { return; }
        if (on == true) 
        {
            Color color = startText.color;
            color.a += Time.deltaTime / timer;
            startText.color = color;
            if (color.a > 1) 
            {
                color.a = 1f;
                runningTimer = 0;
                on = false;
            }
        }
        else
        {
            Color color = startText.color;
            color.a -= Time.deltaTime / timer;
            startText.color = color;
            if (color.a < 0)
            {
                color.a = 0f;
                on = true;
            }
        }
    }
}
