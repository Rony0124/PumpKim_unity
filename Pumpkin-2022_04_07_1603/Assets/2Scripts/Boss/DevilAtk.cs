using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilAtk : MonoBehaviour
{
    public int BossBulletNum;
    //5이상
    public int BossLightningNum;
    public GameObject bullet_fire;
    public GameObject bullet_lightning;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        Vector3 Dir = (PlayerMovement.Instance.GetPos() - transform.position).normalized;
        if (Dir.x < 0)
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Dir.x >= 0)
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    void AttackType1()
    {
        Debug.Log("Attack1");
        StartCoroutine(Fire());

    }
    IEnumerator Fire()
    {
        int num = Random.Range(0, 30);
        for (int i = 0; i < 360; i += BossBulletNum)
        {
            //총알 생성
            GameObject temp = Instantiate(bullet_fire);

            //2초마다 삭제
            Destroy(temp, 2f);

            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = this.transform.position;

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            temp.transform.rotation = Quaternion.Euler(0, 0, i + num);
            yield return new WaitForSeconds(0.05f);
        }
    }
    void AttackType2()
    {
        Debug.Log("Attack2");
        StartCoroutine(Lightning());
    }
    IEnumerator Lightning()
    {
        int num = Random.Range(25, BossLightningNum);
        for (int i = 0; i < num; i++)
        {
            //총알 생성
            GameObject temp = Instantiate(bullet_lightning);

            //2초마다 삭제
            Destroy(temp, 1.5f);

            //생성 위치이식 할때, 룸기준으로 다시짜기
            float offsetX = Random.Range(-6f, 6f);
            float offsetY = Random.Range(-8f, 8f);
            temp.transform.position = new Vector2(this.transform.position.x + offsetX, this.transform.position.y + offsetY);

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            //temp.transform.rotation = Quaternion.Euler(0, 0, i);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
