using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 velocity = new Vector2(1.0f, 0.0f);
    public GameObject Shooter;
 
    Vector3 NewDir;
    int bounceCnt = 1;

    private void Start()
    {
        Shooter = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPos = currentPos + velocity * Time.deltaTime;
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy") || other.transform.CompareTag("Boss"))
        {
            if(PlayerData.Instance.PlayerSkill[3] != 0 && PlayerTargeting.Instance.MonsterList.Count >= 2)
            {
                 int myIndex = PlayerTargeting.Instance.MonsterList.IndexOf(other.gameObject);
                if( bounceCnt > 0)
                {
                    if (PlayerData.Instance.PlayerSkill[2] != 0)
                    {
                        bounceCnt *= 2;
                    }
                    bounceCnt--;
                    NewDir = ResultDir(myIndex) * 10;
                    velocity = NewDir;
                    return;
                }
            }
            BulletEffect();
        }

        if (other.transform.CompareTag("Wall") )
        {
            BulletEffect();
        }
    }

    void BulletEffect()
    {
        GameObject AtkEffect = Instantiate(EffectSet.Instance.PlayerAtkEffect, gameObject.transform.position, Quaternion.identity);
        GameObject AtkEffect1 = Instantiate(EffectSet.Instance.PlayerAtkEffect1, gameObject.transform.position, Quaternion.identity);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Destroy(AtkEffect, 1f);
        Destroy(AtkEffect1, 1f);
        Destroy(gameObject);
    }
    Vector3 ResultDir(int index)
    {
        int closestIndex = -1;
        float closestDist = Mathf.Infinity;
        float currentDist = 0f;

        for (int i = 0; i <PlayerTargeting.Instance.MonsterList.Count; i++)
        {
            if (i == index) continue;

            currentDist = Vector3.Distance(PlayerTargeting.Instance.MonsterList[i].transform.position, transform.position);

            if (currentDist > 5f) continue;
            if(closestDist > currentDist)
            {
                closestDist = currentDist;
                closestIndex = i;
            }
        }

        if (closestIndex == -1)
        {
            Destroy(gameObject);
            return Vector3.zero;
        }
        return (PlayerTargeting.Instance.MonsterList[closestIndex].transform.position - transform.position).normalized;
    }


}
