using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaidInOut : MonoBehaviour
{
    public static FaidInOut Instance;
    [SerializeField] public bool faid = false;//확인용
    float fadeTime = 1.0f;
    Image FadeImg;
    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//루트 게임오브젝트만 넣어줘야 한다
            //삭제가 안됨
            //DontDestroyOnLoad 씬을 에디티브로 생성 후 거기에 넣어줌
        }
        else
        {
            Destroy(gameObject);//예약됨
            return;
        }
        FadeImg = GetComponentInChildren<Image>();
    }


    // Update is called once per frame
    void Update()
    {
        faidImgInOut();
    }

    private void faidImgInOut()
    {
        if (faid == true && FadeImg.color.a < 1)
        {
            Color color = FadeImg.color;
            color.a += Time.deltaTime / fadeTime;
            if (color.a > 1)
            {
                color.a = 1;
            }
            FadeImg.color = color;
        }
        else if (faid == false && FadeImg.color.a > 0) 
        {
            Color color = FadeImg.color;
            color.a -= Time.deltaTime / fadeTime;
            if (color.a < 0)
            {
                color.a = 0;
            }
            FadeImg.color = color;
        }
    }
}
