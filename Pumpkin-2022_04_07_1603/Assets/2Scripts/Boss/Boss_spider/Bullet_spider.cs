
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class Bullet_spider : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bullet2;
    public GameObject miniSpider;
    public int BossBullet;
    public int spiderSpawn;
    private Animator animator;
    Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("IsAttack") == true)
        {
            StartCoroutine("LongDistanceAttack");
            animator.SetBool("IsAttack", false);
        }
        if(animator.GetBool("SpiderSpawn") == true)
        {
            StartCoroutine("SpawnSpider");
            animator.SetBool("SpiderSpawn", false);
        }

    }
    IEnumerator LongDistanceAttack()
    {
        yield return new WaitForSeconds(1.3f);
        shot();
        yield return new WaitForSeconds(1f);
        shot();
    }

    void shot()
    {
        //360번 반복
        for (int i = 0; i < 360; i += BossBullet)
        {
            //총알 생성
            GameObject temp = Instantiate(bullet);

            //2초마다 삭제
            Destroy(temp, 2f);

            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = this.transform.position;

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }
    public void ShootTarget()
    {
        dir = (PlayerMovement.Instance.GetPos() - transform.position).normalized;
        double angle = Mathf.Atan2(dir.y, dir.x);
        angle = angle * 180 / Mathf.PI;
        //Mathf.Round((decimal)angle, 3);
        //Debug.Log(angle);
        GameObject temp = Instantiate(bullet2, transform.position, Quaternion.Euler(0, 0, (float)angle -90f));
        //GameObject temp = Instantiate(bullet2, this.transform.position, Quaternion.identity);
        Destroy(temp, 2f);

        //총알 생성 위치를 (0,0) 좌표로 한다.
        //temp.transform.position = this.transform.position;

    }
    IEnumerator SpawnSpider()
    {
        yield return new WaitForSeconds(1.3f);

        for (int i = 0; i < spiderSpawn; i++)
        {
            //총알 생성
            GameObject temp = Instantiate(miniSpider);

            //2초마다 삭제
            //Destroy(temp, 2f);
            
            //총알 생성 위치를 (0,0) 좌표로 한다.
            float deg = Random.Range(-360f, 360f);

            temp.transform.position = new Vector2(this.transform.position.x + 1f * Mathf.Sin(deg), this.transform.position.y + 1f * Mathf.Cos(deg));
            yield return new WaitForSeconds(0.1f);
            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            // temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }
    void Spawn()
    {
        //360번 반복
        
    }
}
