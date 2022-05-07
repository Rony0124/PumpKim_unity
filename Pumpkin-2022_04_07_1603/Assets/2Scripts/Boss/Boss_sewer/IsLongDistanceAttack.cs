using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLongDistanceAttack : StateMachineBehaviour
{

    public float timer;




    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsLongDistanceAttackCheck", true);
        
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("IsStop");
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }



    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsLongDistanceAttackCheck", false);
    }


}
