using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatas : MonoBehaviour
{
    [Header("플레이어 정보")]
    [SerializeField, Range(1, 10)] int plLevel = 1;//레벨,화면에 표시되게 하고 싶다
    [SerializeField] float exp = 0.0f;//경험치 시작시점
    [SerializeField] float fullExp = 10.0f;//경험치 최대치
    [SerializeField] float ExpPoint = 1.0f;//경험치 포인트
    [SerializeField, Range(0, 5)] int HP = 5;//체력
    float deathTime = 1.0f;
    float deathTimer = 0.0f;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        death();
    }
    /// <summary>
    /// 사망시 연출
    /// </summary>
    private void death() 
    {
        if (HP == 0)//사망시
        {
            anim.SetBool("Death", true);
            deathTimer += Time.deltaTime;
            //Destroy(gameObject);
            if (deathTimer > deathTime) 
            {
               Destroy(gameObject);
            }
        }
    }
}
