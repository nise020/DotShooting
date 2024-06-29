using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("플레이어 위치")]
    [SerializeField] public Transform targetTransform;
     Camera cam;
    [Header("몬스터")]
    [SerializeField] List<GameObject> enemyList;
    [SerializeField] GameObject CreatTab;
    [SerializeField] List<Transform> enemySpownPosiTion;

    [Header("적 생성시간")]
    float enemySpawnTimer = 0.0f;// 타이머
    [SerializeField, Range(0.1f, 5f)] float enemySpawnTime = 1.0f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;//=GameManager가 된다
        }
        cam = Camera.main;
    }
    /// <summary>
    /// transform에 대한 정보를 밖으로 꺼낸다
    /// </summary>
    /// <param name="_pos"></param>
    public void PlayerLocalPosiTion(out Vector3 _pos)
    {
        _pos = targetTransform.position;
    }


}
