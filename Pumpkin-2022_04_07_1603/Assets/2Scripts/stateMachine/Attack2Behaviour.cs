using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2Behaviour : StateMachineBehaviour
{
    private const float STOPPING_DIST = 5f;
    private const float RETREAT_DIST = 3f;

    private Vector3 targetPos;
    private Vector3 pos;
   // private int speed;
    DoctorBoss emController;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetPos = PlayerMovement.Instance.GetPos();
       // emController = animator.gameObject.GetComponent<DoctorBoss>();

        //speed = 2;

    }

    //update
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (emController.roomCondition == null) return;
        //if (emController.roomCondition.playerInThisRoom && !emController.isDead)
        //{
        targetPos = PlayerMovement.Instance.GetPos();
        pos = animator.transform.position;
        if (Vector2.Distance(targetPos, pos) > RETREAT_DIST)
        {
            animator.SetTrigger("isWalk");
            //Debug.Log(Vector2.Distance(targetPos, pos));
            //animator.transform.position = Vector2.MoveTowards(pos, targetPos, speed * Time.deltaTime);
        }
        
        else if (Vector2.Distance(targetPos, pos) <= RETREAT_DIST)
        {
            Debug.Log("W3");
            //animator.transform.position = Vector2.MoveTowards(pos, targetPos, -speed * Time.deltaTime);
            animator.SetTrigger("isAtk1");
        }
        // }

    }
   
}
