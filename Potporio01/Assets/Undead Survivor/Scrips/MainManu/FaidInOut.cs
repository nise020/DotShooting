using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaidInOut : MonoBehaviour
{
    public static FaidInOut Instance;
    [SerializeField] public bool faid = false;//Ȯ�ο�
    float fadeTime = 1.0f;
    Image FadeImg;
    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//��Ʈ ���ӿ�����Ʈ�� �־���� �Ѵ�
            //������ �ȵ�
            //DontDestroyOnLoad ���� ����Ƽ��� ���� �� �ű⿡ �־���
        }
        else
        {
            Destroy(gameObject);//�����
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
