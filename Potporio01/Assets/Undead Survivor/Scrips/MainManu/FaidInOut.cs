using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FaidInOut : MonoBehaviour
{
    public static FaidInOut Instance;
    [SerializeField] public bool fade = false;//확인용
    float fadeTime = 1.0f;
    Image FadeImg;
    UnityAction action = null;
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
        if (fade == true && FadeImg.color.a < 1)//On,밝아지기
        {
            Color color = FadeImg.color;
            color.a += Time.deltaTime / fadeTime;
            if (color.a > 1)
            {
                color.a = 1;
                if (action != null)
                {
                    action.Invoke();
                    action = null;
                }
            }
            FadeImg.color = color;
        }
        else if (fade == false && FadeImg.color.a > 0)//Off,어두워지기
        {
            Color color = FadeImg.color;
            color.a -= Time.deltaTime / fadeTime;
            if (color.a < 0)
            {
                color.a = 0;
            }
            FadeImg.color = color;
        }
        FadeImg.raycastTarget = FadeImg.color.a != 0.0f;
    }
    public void ActiveFade(bool _fade, UnityAction _action)//= null
    {
        fade = _fade;
        action = _action;
    }
}
