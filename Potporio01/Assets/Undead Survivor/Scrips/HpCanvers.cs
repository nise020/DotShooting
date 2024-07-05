using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCanvers : MonoBehaviour
{
    [SerializeField] Image PlayerHPImg;
    PlayerStatas playerStatas;
    GameManager gameManager;
    //float nowHP;
    //float maxHP;
    private void Awake()
    {
        PlayerHPImg.fillAmount = 1;//���۽� ü�� ���󺹱�
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void Update()
    {
        HpBar();
        CheckHp();
        HpPos();
    }

    /// <summary>
    /// ��� �����̴� �÷��̾� ����ٴϱ�
    /// </summary>
    private void HpPos() 
    {
        playerStatas = FindObjectOfType<PlayerStatas>();
        Vector2 posY = playerStatas.transform.position;
        posY.y += 1;
        transform.position = posY;
    }
    public void HpBar(/*float now, float max*/)
    {
        playerStatas = FindObjectOfType<PlayerStatas>();
       float now = playerStatas.NowHp;
       float max = playerStatas.MaximumHP;
        PlayerHPImg.fillAmount = now / max ;
    }
    private void CheckHp() 
    {
        if (PlayerHPImg.fillAmount == 0) 
        {
            Destroy(gameObject);
        }

    }
}
