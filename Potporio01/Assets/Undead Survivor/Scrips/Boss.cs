using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    float coolTime = 10.0f;
    float coolTimer=  0.0f;
    float SkillcoolTime = 30.0f;
    float SkillcoolTimer=  0.0f;

    //private void Mobmoving()
    //{
    //    //if (playerStatas == null) { return; }
    //    Vector3 playerPos;
    //    gameManager.PlayerTrsPosiTion(out playerPos);//��¿�
    //    Vector3 distance = playerPos - MobTrnspos.position;
    //    distance.z = 0.0f;
    //    MobTrnspos.Translate(distance.normalized * moveSpeed * Time.deltaTime);
    //    coolTimer += Time.deltaTime;
    //    if (coolTimer >= coolTime) //���� ��ų ��Ÿ��
    //    {
    //        if (SkillcoolTimer >= SkillcoolTime) 
    //        {

    //        }
    //    }

    //    //Debug.Log(distance);
    //    //Debug.Log($"{distance}");//�����ʿ�

    //}
    //protected override void deathCheck()//���� �ʿ�
    //{
    //    if (HP <= 0)
    //    {
    //        Debug.Log(HP);
    //        anim.SetBool("Death", true);
    //        deathTimer += Time.deltaTime;//�ִϸ��̼� ����ð�
    //        Debug.Log($"deathTimer={deathTimer}");
    //        if ((deathTimer >= deathTime))
    //        {
    //            gameManager.CreateItemCheck(transform.position);
    //            Destroy(gameObject);
    //            deathTimer = 0.0f;
    //        }

    //    }
    //}
}
