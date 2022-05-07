using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorIdleBehaviour : StateMachineBehaviour
{
    private int rand;

    private const float STOPPING_DIST = 5f;
    private const float RETREAT_DIST = 3f;

    private float timer;
    private const float startTimer = 1.5f;
    private Vector3 targetPos;
    private Vector3 pos;
    private int speed;
    DoctorBoss emController;
    //start
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetPos = PlayerMovement.Instance.GetPos();
        emController = animator.gameObject.GetComponent<DoctorBoss>();
        pos = animator.transform.position;
        speed = 2;
       
    }

    //update
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (emController.roomCondition == null) return;
        //if (emController.roomCondition.playerInThisRoom && !emController.isDead)
       // {
            //pos = transform.position;
        if (timer <= 0)
        {
            if (Vector2.Distance(targetPos, pos) > STOPPING_DIST)
            {
                Debug.Log("1");
                animator.transform.position = Vector2.MoveTowards(pos, targetPos, speed * Time.deltaTime);
                animator.SetTrigger("isWalk");
            }
            else if (Vector2.Distance(targetPos, pos) < STOPPING_DIST && Vector2.Distance(targetPos, pos) > RETREAT_DIST)
            {
                Debug.Log("2");
                animator.transform.position = Vector2.MoveTowards(pos, targetPos, speed * Time.deltaTime);
                animator.SetTrigger("isAtk2");
                //to keep poiting position as given condition is met
                animator.transform.position = animator.transform.position;

            }
            else if (Vector2.Distance(targetPos, pos) < RETREAT_DIST)
            {
                Debug.Log("3");
                //animator.transform.position = Vector2.MoveTowards(pos, targetPos, -speed * Time.deltaTime);
                animator.SetTrigger("isAtk1");
            }
            timer = startTimer;
        }else
        {
            timer -= Time.deltaTime;
        }
           
       // }
       
    }

    //한번만 실행
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    
}
