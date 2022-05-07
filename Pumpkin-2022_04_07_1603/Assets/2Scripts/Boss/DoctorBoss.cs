using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoctorBoss : EnemyBase
{
    public GameObject ax;
    public GameObject electricShock;
    Vector2 dir;
    //public int BossBullet;
    private void Awake()
    {
        AtkTarget = GetComponent<IAtkTarget>();
    }
    // Start is called before the first frame update
    void Start()
    {
        color = gameObject.GetComponent<SpriteRenderer>().color;
        if (gameObject.transform.parent == null) return;
        //roomCondition = gameObject.transform.parent.GetComponent<RoomCondition>();

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = PlayerMovement.Instance.GetPos();
        dir = (playerPos - transform.position).normalized;
        //Debug.Log(dir);
        /*i*//*f (roomCondition == null) return;
        if (roomCondition.playerInThisRoom && !isDead)
        {
            StartCoroutine(Wait4Player());
            playerPos = PlayerMovement.Instance.GetPos();
        }*/
        
    }
    IEnumerator Wait4Player()
    {
        yield return new WaitForSeconds(WAIT4PLAYER);
        AtkTarget.AtkTarget(playerPos, transform.position, (int)speed);

    }
    void SetStop()
    {
        GetComponent<Animator>().SetTrigger("isStop");
    }
    void Atk()
    {
        double angle = Math.Atan2(dir.y, dir.x);
        
        angle = angle * 180 / Math.PI;
        Math.Round((decimal)angle, 3);
        Instantiate(ax, PlayerMovement.Instance.GetPos(), Quaternion.Euler(0, 0, (float)angle + 180f) );
    }
    void ElectrickShock()
    {
        Instantiate(electricShock, transform.position, Quaternion.identity);
    }
  
}
