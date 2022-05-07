using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBehaviour : StateMachineBehaviour
{
    private int rand;
    public float timer; //얼마나 재생시킬것인지 시간
    //start
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("IsWalk", true);
        //animator.SetTrigger("IsSpiderAttack");
        rand = Random.Range(0, 3);

         if (rand == 0)
         {
             animator.SetTrigger("IsSpiderAttack");
         }
         else if (rand == 1)
         {
             animator.SetTrigger("IsWalk");
         }
         else
         {
             animator.SetTrigger("IsWebAttack");
         }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
