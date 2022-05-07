using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class BossController : EnemyBase
{

    static BossController instance;
    public static BossController Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake() 
    { 
        instance = this;
        float x = Mathf.Pow(1.2f, (StageMng.Instance.currentStage - 1));
        if (StageMng.Instance.currentEp == 2)
        {
            maxHP *= 1.3f;
        }
        else if (StageMng.Instance.currentEp == 3)
        {
            maxHP *= 4f;
        }

        maxHP *= x;
        currentHP = maxHP;
    }
    public BossRoomCondition bossRoom;
    //public Sprite HpIcon;
    private void Start()
    {
        color = gameObject.GetComponent<SpriteRenderer>().color;
        if (gameObject.transform.parent == null) return;
        bossRoom = gameObject.transform.parent.GetComponent<BossRoomCondition>();

    }
  
   

   
  

   

}
