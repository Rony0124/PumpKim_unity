using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBehavior : StateMachineBehaviour
{
    private int rand;

    //start
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        rand = Random.Range(0, 3);

        if (rand == 0)
        {
            animator.SetTrigger("IsLongDistanceAttack");
        }
        else if (rand == 1)
        {
            animator.SetTrigger("IsWalk");
        }
        else
        {
            animator.SetTrigger("IsRushAttack");
        }
    }

   

    //한번만 실행
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
