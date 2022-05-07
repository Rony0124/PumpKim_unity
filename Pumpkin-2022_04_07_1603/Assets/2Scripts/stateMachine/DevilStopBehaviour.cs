using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilStopBehaviour : StateMachineBehaviour
{
    private int rand;
    float timer;
    float startTime = 1.5f;
    //start
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    //update
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timer <= 0)
        {
            rand = Random.Range(0, 2);
            if (rand == 0)
            {
                animator.SetTrigger("Attack1");
            }
            else
            {
                animator.SetTrigger("Attack2");
            }
            timer = startTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    //한번만 실행
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     

    
    }
}
