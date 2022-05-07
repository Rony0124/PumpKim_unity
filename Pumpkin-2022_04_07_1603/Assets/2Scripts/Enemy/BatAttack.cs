using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BatAttack : MonoBehaviour, IAtkTarget
{
   
    public GameObject bullet;
    public GameObject bullet2;
    public int BossBullet;
    Vector2 dir;
    
  
    void Atk()
    {
        dir = (PlayerMovement.Instance.GetPos() - transform.position).normalized;
        double angle = Math.Atan2(dir.x, dir.y);
        angle = -angle * 180 / Math.PI;
        Math.Round((decimal)angle, 3);
        //Debug.Log(angle);
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, (float)angle));
        //Instantiate(bullet, transform.position, Quaternion.identity);
    }
    void shot()
    {
        //360번 반복
        for (int i = 0; i < 360; i += BossBullet)
        {
            //총알 생성
            GameObject temp = Instantiate(bullet2);

            //2초마다 삭제
            Destroy(temp, 2f);

            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = this.transform.position;

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            temp.transform.rotation = Quaternion.Euler(0, 0, i - 90);
        }
    }
    public void AtkTarget()
    {
        gameObject.GetComponent<Animator>().SetBool("isAtk", true);
    }
    public void AtkTarget(Vector3 _targetPos, Vector3 _pos, int _speed) 
    {
        /*//targetPos = _targetPos;
        //pos = _pos;
        speed = _speed;*/
    }
}
