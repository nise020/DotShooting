using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatas : MonoBehaviour
{
    MoveControll moveControll;

    [Header("�÷��̾� ����")]
    [SerializeField, Range(1, 10)] int plLevel = 1;//����,ȭ�鿡 ǥ�õǰ� �ϰ� �ʹ�
    [SerializeField] float exp = 0.0f;//����ġ ���۽���
    [SerializeField] float fullExp = 10.0f;//����ġ �ִ�ġ
    [SerializeField] float ExpPoint = 1.0f;//����ġ ����Ʈ
    [SerializeField, Range(0, 5)] public int NowHp = 5;//�ð������� ���̴� ü��

    private int MaximumHP = 5;
    float deathTime = 1.0f;
    float deathTimer = 0.0f;
    Animator anim;
    public bool Alive =true;//���� ����
    Transform trsmagnet;
    int magnetDrop = 0;
    public bool MgItem = false;
    CircleCollider2D mgcircle;
    private void Awake()
    {
        trsmagnet = transform;
        mgcircle = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Mob"))//�����ð� ���� ����
        {
            if (NowHp <= MaximumHP)//������
            {
                NowHp = NowHp - 1;//������    
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Heal"))//item
        {
            if (NowHp <= MaximumHP)//��
            {
                NowHp += 1;
                Debug.Log(NowHp);
            }
            else 
            {
                NowHp = MaximumHP;//ü�� ���� ����
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Speed"))//item
        {

        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Magnet"))//item
        {
            MgItem = true;
            if (MgItem == true) 
            {
                mgcircle.radius += 0.1f;
                MgItem = false;
            }
        }
    }

    //collision.tag == Tool.GetTag(GameTags.Item)
    private void Update()
    {
        deathCheck();
    }
    /// <summary>
    /// ����� ����
    /// </summary>
    private void deathCheck() //Ʈ���� �ʿ�
    {
        if (NowHp == 0 && Alive == true)
        {
            //Alive = false;
            anim = GetComponent<Animator>();
            anim.SetBool("Death", true);
            deathTimer += Time.deltaTime;
            Debug.Log(deathTimer);
            if (deathTimer >= deathTime)
            {
                gameObject.SetActive(false);//���� �ʿ�
                Alive = false;
            }
        }
        
    }
}
