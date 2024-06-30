using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[Header("������Ʈ ��")]

    public static GameManager Instance;
    [Header("�÷��̾� ��ġ")]
    [SerializeField] public Transform trsTarget;
     Camera cam;

    [Header("���� ����")]
    [SerializeField] List<GameObject> mobList;
    [SerializeField] Transform CreatTab;
    [SerializeField] List<Vector2> createLine;
    private bool isSpawn = false;

    [Header("�� �����ð�")]
    float mobSpawnTimer = 0.0f;// Ÿ�̸�
    [SerializeField, Range(0.1f, 5f)] float mobSpawnTime = 1.0f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;//=GameManager�� �ȴ�
        }
        cam = Camera.main;
    }
    private void Update()
    {
        createEnemy();//Ȱ��ȭ �ʿ�
    }
    /// <summary>
    /// �÷��̾ �������� �����Ÿ� �ۿ��� �����ϴ� ���
    /// </summary>
    private void createEnemy()
    {
        if (trsTarget == null) { return; }
        //�÷��̾� ��ġ ������

        mobSpawnTimer += Time.deltaTime;//Ÿ�̸� ON
        if (mobSpawnTimer > mobSpawnTime)//Ÿ�̸Ӱ� ���� ��Ÿ���� �Ѿ��� ���
        {
            int Iroad = Random.Range(0, 4);//������ ��ġ�� ��輱 ����
            //Random.Range(1, 4)1,2,3 

            Vector2 playerPos = trsTarget.position;//player ��ġ
            Vector2 done = new Vector2(0,0); //����Ʈ ����

            float Randamx = Random.Range(-8.0f, 8.0f);//x�� ������ġ ����
            float Randamy = Random.Range(-4.0f, 4.0f);//y�� ������ġ ����

            if (Iroad == 0) //10
            {
                PlusMinas(playerPos.x);//������ ������ ��ȯ
                PlusMinas(playerPos.y);//������ ������ ��ȯ
                done = new Vector2(createLine[0].x + playerPos.x, Randamy + playerPos.y);
            }
            else if (Iroad == 1)//-10
            {
                PlusMinas(playerPos.x);
                PlusMinas(playerPos.y);
                done = new Vector2(createLine[1].x + playerPos.x, Randamy + playerPos.y);
            }
            else if (Iroad == 2)//7
            {
                PlusMinas(playerPos.x);
                PlusMinas(playerPos.y);
                done = new Vector2(Randamx + playerPos.x, createLine[2].y + playerPos.y);
            }
            else//Iroad == 3//-7
            {
                PlusMinas(playerPos.x);
                PlusMinas(playerPos.y);
                done = new Vector2(Randamx + playerPos.x, createLine[3].y + playerPos.y);
            }
            //done.x = Mathf.Clamp(done.x, -10, 10);
            //done.y = Mathf.Clamp(done.y, -7, 7);

            int mobcount = mobList.Count;
            int mobiRoad = Random.Range(0, mobcount);

            GameObject go = Instantiate(mobList[mobiRoad], done, Quaternion.identity, CreatTab);
            mobSpawnTimer = 0.0f;
            #region ������ ����ó�� �밡��
            //int count = 1;
            //for (int INum = 0; INum < count; INum++)
            //{
            //    if (Iroad == 1)//x=10
            //    {
            //        if (defoltPos.x < 0.0f && defoltPos.y < 0.0f)// -/-
            //        {
            //            defoltPos.x *= defoltPos.x + createLine[0].x;
            //            defoltPos.y *= defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y < 0.0f)//+/-
            //        {
            //            defoltPos.x = defoltPos.x + createLine[0].x;
            //            defoltPos.y *= defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y > 0.0f)//+/+
            //        {
            //            defoltPos.x = defoltPos.x + createLine[0].x;
            //            defoltPos.y = defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }
            //        else if (defoltPos.x < 0.0f && defoltPos.y > 0.0f)//-/+
            //        {
            //            defoltPos.x *= defoltPos.x + createLine[0].x;
            //            defoltPos.y = defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }

            //    }
            //    else if (Iroad == 2)//x=-10
            //    {
            //        if (defoltPos.x < 0.0f && defoltPos.y < 0.0f)// -/-
            //        {
            //            defoltPos.x = defoltPos.x + createLine[1].x;
            //            defoltPos.y = defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y < 0.0f)//+/-
            //        {
            //            defoltPos.x *= defoltPos.x + createLine[1].x;
            //            defoltPos.y = defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y > 0.0f)//+/+
            //        {
            //            defoltPos.x *= defoltPos.x + createLine[1].x;
            //            defoltPos.y *= defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }
            //        else if (defoltPos.x < 0.0f && defoltPos.y > 0.0f)//-/+
            //        {
            //            defoltPos.x = defoltPos.x + createLine[1].x;
            //            defoltPos.y += defoltPos.y + Random.Range(-4.0f, 4.0f);
            //        }
            //    }
            //    else if (Iroad == 3)//y=7
            //    {
            //        if (defoltPos.x < 0.0f && defoltPos.y < 0.0f)// -/-
            //        {
            //            defoltPos.y *= defoltPos.y + createLine[2].y;
            //            defoltPos.x *= defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y < 0.0f)//+/-
            //        {
            //            defoltPos.y = defoltPos.y + createLine[2].y;
            //            defoltPos.x *= defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y > 0.0f)//+/+
            //        {
            //            defoltPos.y = defoltPos.y + createLine[2].y;
            //            defoltPos.x = defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }
            //        else if (defoltPos.x < 0.0f && defoltPos.y > 0.0f)//-/+
            //        {
            //            defoltPos.y *= defoltPos.y + createLine[2].y;
            //            defoltPos.x = defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }

            //    }
            //    else if (Iroad == 4)//y=7
            //    {
            //        if (defoltPos.x < 0.0f && defoltPos.y < 0.0f)// -/-
            //        {
            //            defoltPos.y = defoltPos.y + createLine[2].y;
            //            defoltPos.x = defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y < 0.0f)//+/-
            //        {
            //            defoltPos.y *= defoltPos.y + createLine[2].y;
            //            defoltPos.x = defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }
            //        else if (defoltPos.x > 0.0f && defoltPos.y > 0.0f)//+/+
            //        {
            //            defoltPos.y *= defoltPos.y + createLine[2].y;
            //            defoltPos.x *= defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }
            //        else if (defoltPos.x < 0.0f && defoltPos.y > 0.0f)//-/+
            //        {
            //            defoltPos.y = defoltPos.y + createLine[2].y;
            //            defoltPos.x *= defoltPos.x + Random.Range(-8.0f, 8.0f);
            //        }
            //    }
            //    done = defoltPos;
            //    if ((done.x > 10 && done.x > -10) ||
            //        (done.y > 7 && done.y > -7))
            //    {
            //        //y=4//x=8
            //        defoltPos = targetTransform.position;
            //        INum = 0;
            //        continue;
            //    }
            //    done = defoltPos;
            //}
            #endregion

            #region �����̴� �ּ�ó���Ѱ�
            //GameObject go = Instantiate(mobList[mobiRoad], defoltPos,Quaternion.identity, CreatTab);
            // GameObject poroses01 = Instantiate(mobList[mobiRoad], defoltPos,
            // Quaternion.identity, CreatTab);
            //GameObject poroses02 = Instantiate(mobList[mobiRoad], CreatTab);
            //Enemy gosc = go.GetComponent<Enemy>();
            //CameraPosition,
            #endregion
        }
        


    }
    /// <summary>
    /// ������ �������� ����
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>����</returns>
    public float PlusMinas(float pos)
    {
        if (pos > 0) 
        {
            return pos;
        }
        else if (pos < 0) 
        {
            pos = Mathf.Abs(pos);
        }
        return pos;
    }

    /// <summary>
    /// transform�� ���� ������ ������ ������
    /// </summary>
    /// <param name="_pos"></param>
    public void PlayerTrsPosiTion(out Vector3 _pos)
    {
        _pos = trsTarget.position;
    }


}
