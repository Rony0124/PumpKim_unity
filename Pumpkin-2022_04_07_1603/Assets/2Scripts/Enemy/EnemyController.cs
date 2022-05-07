using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : EnemyBase
{
    public ParticleSystem dust;
    public bool isIn = true;
    public RoomCondition roomCondition;
    void Start()
    {
        color = gameObject.GetComponent<SpriteRenderer>().color;
        if (gameObject.transform.parent == null) return;
        roomCondition = gameObject.transform.parent.GetComponent<RoomCondition>();
    }

    void Update()
    {
        if (roomCondition == null) return;
        if (roomCondition.playerInThisRoom && isIn)
        {
            isIn = false;
            StartCoroutine(Wait4Player());
            if (dust != null)
            {
                dust.gameObject.SetActive(true);
            }
        }
        if (isHeadShot)
        {
            currentHP = -1f;
        }

    }
   
    IEnumerator Wait4Player()
    {
        yield return new WaitForSeconds(1f);
        //enemy 애니메이션 트리거셋팅
        gameObject.GetComponent<Animator>().SetBool("isAtk", true);
        
    }
    
   
    
   

 
}
