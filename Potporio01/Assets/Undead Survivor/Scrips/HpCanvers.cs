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
    //float nowHP;
    //float maxHP;
    private void Awake()
    {
        PlayerHPImg.fillAmount = 1;//시작시 체력 원상복구
    }
    private void Start()
    {
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
    /// 계속 움직이는 플레이어 따라다니기
    /// </summary>
    private void HpPos() 
    {
        if (playerStatas == null) { return; }
        gameManager.PlayerTrsPosiTion(out Vector3 pos);
        pos.y += 1;
        transform.position = pos;  
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
