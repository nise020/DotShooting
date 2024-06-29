using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("�÷��̾� ��ġ")]
    [SerializeField] public Transform targetTransform;
     Camera cam;
    [Header("����")]
    [SerializeField] List<GameObject> enemyList;
    [SerializeField] GameObject CreatTab;
    [SerializeField] List<Transform> enemySpownPosiTion;

    [Header("�� �����ð�")]
    float enemySpawnTimer = 0.0f;// Ÿ�̸�
    [SerializeField, Range(0.1f, 5f)] float enemySpawnTime = 1.0f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;//=GameManager�� �ȴ�
        }
        cam = Camera.main;
    }
    /// <summary>
    /// transform�� ���� ������ ������ ������
    /// </summary>
    /// <param name="_pos"></param>
    public void PlayerLocalPosiTion(out Vector3 _pos)
    {
        _pos = targetTransform.position;
    }


}
