using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCanvers : MonoBehaviour
{
    [SerializeField] Image PlayerHPImg;
    [SerializeField] Image skillImg;
    PlayerStatas playerStatas;
    GameManager gameManager;
    Transform childtrs;
    RectTransform rectTransform;
    //float nowHP;
    //float maxHP;
    private void Awake()
    {
        PlayerHPImg.fillAmount = 1;//���۽� ü�� ���󺹱�
    }
    private void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        gameManager = GameManager.Instance;
        playerStatas = FindObjectOfType<PlayerStatas>();

    }
    private void Update()
    {
        if (gameManager.objStop == true) { return; }
        HpPos();
        HpBar();
        CheckHp();
    }

    /// <summary>
    /// ��� �����̴� �÷��̾� ����ٴϱ�
    /// </summary>
    private void HpPos() 
    {
        if (playerStatas == null) { return; }
        gameManager.PlayerTrsPosiTion(out Vector3 pos);
        pos = pos + Vector3.up;
        rectTransform.anchoredPosition = pos;
        //transform.localPosition = pos;
        //transform.position = pos;
        //Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
        //Debug.Log("Screen Position: " + screenPos);
        // RectTransform�� ���� ȭ�� ��ǥ�� ����
        //RectTransform value = GetComponent<RectTransform>();
        //value.position = screenPos;s
    }
    public void HpBar()
    {
       float now = playerStatas.NowHp;
       float max = playerStatas.MaximumHP;
        PlayerHPImg.fillAmount = now / max ;
    }
    private void CheckHp() 
    {
        if (PlayerHPImg.fillAmount < 0.1) 
        {
            Destroy(gameObject);
        }

    }

}
