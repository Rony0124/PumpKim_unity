using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttackBehaviour : StateMachineBehaviour
{
    public float timer;

    public const float START_TIME = 2f;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.SetBool("IsLongDistanceAttackCheck", true);
        timer = START_TIME;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetBool("isAtk", false);
            timer = START_TIME;
           // animator.SetBool("IsLongDistanceAttackCheck", false);
        }
        else
        {
            timer -= Time.deltaTime;
            
        }
    }



    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
   
}
