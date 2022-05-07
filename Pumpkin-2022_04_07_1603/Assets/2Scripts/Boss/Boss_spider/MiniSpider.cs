using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSpider : MonoBehaviour
{
   // public ParticleSystem dust;


    void Start()
    {
        
    }

    void Update()
    {
        if (PlayerTargeting.Instance.TargetIndex == -1) return;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("부ㄷㅊ");
            GetComponent<EnemyController>().Damgage(10000f);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("부ㄷㅊ");
            GetComponent<EnemyController>().Damgage(10000f);

        }
    }
}
