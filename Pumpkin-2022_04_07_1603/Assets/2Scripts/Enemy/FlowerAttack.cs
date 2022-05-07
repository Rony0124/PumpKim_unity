using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlowerAttack : MonoBehaviour, IAtkTarget
{
    public GameObject FallingAtk;
    public GameObject bullet;
    Vector2 dir;
    void Atk()
    {
        dir = (PlayerMovement.Instance.GetPos() - transform.position).normalized;
        double angle = Math.Atan2(dir.x, dir.y);
        angle = -angle * 180 / Math.PI;
        Math.Round((decimal)angle, 3);
        //Debug.Log(angle);
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, (float)angle));
        AtkTarget(PlayerMovement.Instance.GetPos(), transform.position, 1);
    }
    public void AtkTarget()
    {
        gameObject.GetComponent<Animator>().SetBool("isAtk", true);//AtkTarget(Vector3.zero, Vector3.zero, 0);
    }
    public void AtkTarget(Vector3 targetPos, Vector3 pos, int speed)
    {
        AtkTarget();
        StartCoroutine(FallingAttack(targetPos));
    }
    IEnumerator FallingAttack(Vector3 _targetPos)
    {
        yield return new WaitForSeconds(2.5f);
        Instantiate(FallingAtk, _targetPos, Quaternion.identity);
       
    }
}
