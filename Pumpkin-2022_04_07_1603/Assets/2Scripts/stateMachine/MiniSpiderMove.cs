using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSpiderMove : StateMachineBehaviour
{
    public float speed; //플레이어가 적에게 다가가는 속도
    private Transform playerPos; //플레이어 위치
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //target = new Vector2(playerPos.transform.position.x - animator.transform.position.x, playerPos.transform.position.y - animator.transform.position.y).normalized;

        //animator.SetFloat("DirX", target.x);
        //animator.SetFloat("DirY", target.y);

        
       

      //플레이어를 향해 이동
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);
        
       



    }
    
}
