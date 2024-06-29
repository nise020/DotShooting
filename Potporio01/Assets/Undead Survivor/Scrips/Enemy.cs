using System;
using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    
    GameManager gameManager;
    Vector3 moveDir;
    [SerializeField] float moveSpeed = 0.5f;
    private Transform thisTransform;
    private float beforeX;
    private void Awake()
    {
        // transform ������Ʈ ��������� �ʱ�ȭ
        thisTransform = transform;
        gameManager = FindObjectOfType<GameManager>();
        beforeX = thisTransform.position.x;//������ x�� Ȯ��
    }


    private void Update()
    {
        Mobmoving();
        seeCheack();
    }


    
    /// <summary>
    /// �÷��̾ �ִ� �������� �ڵ����� �̵��մϴ�
    /// </summary>
    private void Mobmoving() 
    {
        Vector3 playerPos;
       
        if (gameManager.targetTransform == null)//player�� �׾��� ���
        {
            return;
        }
        else 
        {
            gameManager.PlayerLocalPosiTion(out playerPos);//��¿�
            Vector3 distance = playerPos - thisTransform.position;
            thisTransform.Translate(distance.normalized * moveSpeed * Time.deltaTime);
            //Debug.Log($"{distans}");

        }

    }
    /// <summary>
    /// �̵��ϴ� ���⿡ ���� Scale�� ������ �¿�� �̵��ϴ°� ó�� ���̰� �Ѵ�
    /// </summary>
    private void seeCheack()
    {
        Vector3 scale = thisTransform.localScale;
        float affterX = thisTransform.position.x;
       
        if (affterX > beforeX)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else if (affterX < beforeX)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
        beforeX = affterX;

    }
}
