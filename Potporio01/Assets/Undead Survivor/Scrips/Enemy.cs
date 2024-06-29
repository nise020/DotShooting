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
        // transform 컴포넌트 명시적으로 초기화
        thisTransform = transform;
        gameManager = FindObjectOfType<GameManager>();
        beforeX = thisTransform.position.x;//기존에 x값 확인
    }


    private void Update()
    {
        Mobmoving();
        seeCheack();
    }


    
    /// <summary>
    /// 플레이어가 있는 방향으로 자동으로 이동합니다
    /// </summary>
    private void Mobmoving() 
    {
        Vector3 playerPos;
       
        if (gameManager.targetTransform == null)//player가 죽었을 경우
        {
            return;
        }
        else 
        {
            gameManager.PlayerLocalPosiTion(out playerPos);//출력용
            Vector3 distance = playerPos - thisTransform.position;
            thisTransform.Translate(distance.normalized * moveSpeed * Time.deltaTime);
            //Debug.Log($"{distans}");

        }

    }
    /// <summary>
    /// 이동하는 방향에 따라서 Scale을 조절해 좌우로 이동하는것 처럼 보이게 한다
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
