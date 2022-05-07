using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorWalkBehaviour : StateMachineBehaviour
{
    private const float STOPPING_DIST = 5f;
    private const float RETREAT_DIST = 3f;

    private Vector3 targetPos;
    private Vector3 pos;
    private float speed;
    float timer;
    float startTimer = 1f;
    DoctorBoss emController;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetPos = PlayerMovement.Instance.GetPos();
        emController = animator.gameObject.GetComponent<DoctorBoss>();
       
        speed = 3.5f;
       
    }

    //update
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (emController.roomCondition == null) return;
        //if (emController.roomCondition.playerInThisRoom && !emController.isDead)
        //{

        targetPos = PlayerMovement.Instance.GetPos();
        pos = animator.transform.position;
        animator.transform.position = Vector2.MoveTowards(pos, targetPos, speed * Time.deltaTime);
       /* if (Vector2.Distance(targetPos, pos) > STOPPING_DIST)
        {
            Debug.Log(Vector2.Distance(targetPos, pos));
                
        }*/
       if(timer <= 0)
        {
            if (Vector2.Distance(targetPos, pos) < STOPPING_DIST && Vector2.Distance(targetPos, pos) > RETREAT_DIST)
            {
                Debug.Log("W2");
                animator.SetTrigger("isAtk2");
                //to keep poiting position as given condition is met
                animator.transform.position = animator.transform.position;

            }
            timer = startTimer;
        }else
        {
            timer -= Time.deltaTime;
        }
       
        if (Vector2.Distance(targetPos, pos) < RETREAT_DIST)
        {
            Debug.Log("W3");
            //animator.transform.position = Vector2.MoveTowards(pos, targetPos, -speed * Time.deltaTime);
            animator.SetTrigger("isAtk1");
        }
       // }

    }

}
