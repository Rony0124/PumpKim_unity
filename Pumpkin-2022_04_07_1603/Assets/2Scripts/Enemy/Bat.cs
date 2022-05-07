using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    private const float STOPPING_DIST = 3f;
    private const float RETREAT_DIST = 2f;

    //IAtkTarget AtkTarget;
    Vector2 targetPos;
    Vector2 pos;
    public int speed;

  

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            targetPos = PlayerMovement.Instance.GetPos();
            pos = transform.position;
            if (Vector2.Distance(targetPos, pos) > STOPPING_DIST)
            {
                //Debug.Log("1");
                //Debug.Log(targetPos);
                transform.position = Vector2.MoveTowards(pos, targetPos, speed * Time.deltaTime);
                gameObject.GetComponent<Animator>().SetBool("isAtk", true);
            }
            else if (Vector2.Distance(targetPos, pos) < STOPPING_DIST && Vector2.Distance(targetPos, pos) > RETREAT_DIST)
            {
                //Debug.Log("2");
                gameObject.GetComponent<Animator>().SetBool("isAtk", true);
                
                transform.position = this.transform.position;

            }
            else if (Vector2.Distance(targetPos, pos) < RETREAT_DIST)
            {
                //Debug.Log("3");
                transform.position = Vector2.MoveTowards(pos, targetPos, -speed * Time.deltaTime);
                //gameObject.GetComponent<Animator>().SetBool("isAtk", false);
            }
        }
       
    }
}
