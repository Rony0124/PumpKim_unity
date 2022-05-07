using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //발사될 총알 오브젝트
    public GameObject bullet;
    public int BossBullet;

    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {

        if (animator.GetBool("IsLongDistanceAttackCheck") == true)
        {
            StartCoroutine("LongDistanceAttack");
            animator.SetBool("IsLongDistanceAttackCheck", false);
        }

        
    }

    IEnumerator LongDistanceAttack()
    {
        yield return new WaitForSeconds(1.3f);
        shot();
        yield return new WaitForSeconds(1f);
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
}
