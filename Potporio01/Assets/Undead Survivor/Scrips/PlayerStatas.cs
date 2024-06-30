using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatas : MonoBehaviour
{
    [Header("�÷��̾� ����")]
    [SerializeField, Range(1, 10)] int plLevel = 1;//����,ȭ�鿡 ǥ�õǰ� �ϰ� �ʹ�
    [SerializeField] float exp = 0.0f;//����ġ ���۽���
    [SerializeField] float fullExp = 10.0f;//����ġ �ִ�ġ
    [SerializeField] float ExpPoint = 1.0f;//����ġ ����Ʈ
    [SerializeField, Range(0, 5)] int HP = 5;//ü��
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
    /// ����� ����
    /// </summary>
    private void death() 
    {
        if (HP == 0)//�����
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
