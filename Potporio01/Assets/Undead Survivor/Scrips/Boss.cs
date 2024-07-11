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
    //    gameManager.PlayerTrsPosiTion(out playerPos);//출력용
    //    Vector3 distance = playerPos - MobTrnspos.position;
    //    distance.z = 0.0f;
    //    MobTrnspos.Translate(distance.normalized * moveSpeed * Time.deltaTime);
    //    coolTimer += Time.deltaTime;
    //    if (coolTimer >= coolTime) //보스 스킬 쿨타임
    //    {
    //        if (SkillcoolTimer >= SkillcoolTime) 
    //        {

    //        }
    //    }

    //    //Debug.Log(distance);
    //    //Debug.Log($"{distance}");//수정필요

    //}
    //protected override void deathCheck()//수정 필요
    //{
    //    if (HP <= 0)
    //    {
    //        Debug.Log(HP);
    //        anim.SetBool("Death", true);
    //        deathTimer += Time.deltaTime;//애니메이션 재생시간
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
