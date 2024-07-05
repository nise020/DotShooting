using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static TagorDataList;

public class PlayerStatas : MonoBehaviour
{
    Enemy enemy;
    HpCanvers hpCanvers;
    Animator anim;
    MoveControll moveControll;
    //GameManager gameManager;

    [Header("�÷��̾� ����")]
    [SerializeField, Range(1, 10)] int plLevel = 1;//����,ȭ�鿡 ǥ�õǰ� �ϰ� �ʹ�
    public float NowHp = 5;//�ð������� ���̴� ���� ü��
    public float MaximumHP = 5;//�ִ� ü��
    int beforHP;
    [SerializeField] int score = 0;//����
    float deathTime = 1.0f;//���ó�� �ð�
    float deathTimer = 0.0f;//
    [Header("�÷��̾� ����")]
    public bool Alive = true;//���� ����
    public bool DropGun = false;
    public bool DropDfGun = false;//�ڵ� ���� ��� ����
    //[SerializeField] float expGaigi = 0.0f;//����ġ ����Ʈ
    //[SerializeField] float MaiximumExpGaigi = 10.0f;//����ġ �ִ�ġ
    //int magnetDrop = 0;//�ڷ� ���׷��̵�
    //public bool MagnetItem = false;//�ڷ�ON
    //public bool DropGun = false;
    //public bool DropGun = false;

    //CircleCollider2D mgcircle;
 
    private void OnValidate()
    {
        //beforHP
        //NowHp = MaximumHP;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Mob"))//�����ð� ���� ����
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null) 
            {
                int damage = 0;
                enemy.MobDemageCheack(out damage);
                NowHp -= damage;
                //GameManager.Instance.Hpcheck(NowHp, MaximumHP);//�ϴ� ���� ����
                Debug.Log($"NowHp={NowHp}");
            }
            //if (NowHp <= MaximumHP)//������
            //{

            //}
            //else { return; }
        }
        //if (collision.gameObject.layer == LayerMask.NameToLayer("EXP"))//item
        //{
        //    expGaigi += 1.0f;//����ġ ����
        //    //Debug.Log($"expGaigip={expGaigi}");
        //    if (expGaigi > MaiximumExpGaigi)
        //    {
        //        expGaigi = 0.0f;//����ġ �ʱ�ȭ
        //        MaiximumExpGaigi += MaiximumExpGaigi;//����ġ �ִ�ġ ����
        //        Debug.Log($"MaiximumExpGaigi={MaiximumExpGaigi}");
        //    }
        //    else { return; }
        //}
        if (collision.gameObject.layer == LayerMask.NameToLayer("Heal"))//item
        {
            if (NowHp < MaximumHP)//��
            {
                NowHp += 1;
                //GameManager.Instance.Hpcheck(NowHp, MaximumHP);
                Debug.Log($"NowHp={NowHp}");
            }
            else { return; }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Speed"))//���ǵ�
        {
            moveControll = GetComponent<MoveControll>();
            if (moveControll.MaxiumSpeed > moveControll.moveSpeed)
            {
                moveControll.moveSpeed += 0.5f;
            }
            else { return; }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("SwordScaleUP"))//ũ�� ����
        {

        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("SwordUgaid"))//���� ����
        {

        }
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Magnet"))//����ġ ���Ƶ��̱�
        //{
        //    MagnetItem = true;
        //    Destroy(collision.gameObject);
        //    if (MagnetItem==true) 
        //    {
        //        MagnetItem = false;
        //        //expDeta.DrainEXP();
        //    }
        //    ////MgItem = true;
        //    //if (MgItem == true) 
        //    //{
        //    //    mgcircle.radius += 0.1f;
        //    //    MgItem = false;
        //    //}
        //}
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
        if (NowHp <= 0 && Alive == true)
        {
            //Alive = false;
            anim = GetComponent<Animator>();
            anim.SetBool("Death", true);
            deathTimer += Time.deltaTime;
            Debug.Log(deathTimer);
            if (deathTimer > deathTime)
            {
                Alive = false;
                Destroy(gameObject);

                //gameObject.SetActive(false);//���� �ʿ�
            }
        }
        
    }
}
