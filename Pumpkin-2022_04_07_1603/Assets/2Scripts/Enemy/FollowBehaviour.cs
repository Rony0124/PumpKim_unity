using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public class FollowBehaviour : StateMachineBehaviour
{
    public float speed;
    public float time;
    public const float START_TIME = 1.5f;
    public Vector3 playerPos;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 Dir = (PlayerMovement.Instance.GetPos() - animator.transform.position).normalized;
        //StartCoroutine(Reload());
        //playerPos = animator.transform.position;
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else 
        {
            playerPos = PlayerMovement.Instance.GetPos();
            if (Dir.x > 0)
            {
                animator.transform.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                animator.transform.GetComponent<SpriteRenderer>().flipX = false;
            }
            time = START_TIME;
        }
       
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos, speed * Time.deltaTime);
        
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

}
