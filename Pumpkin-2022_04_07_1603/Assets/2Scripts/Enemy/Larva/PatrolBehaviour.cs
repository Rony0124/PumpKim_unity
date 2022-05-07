using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    //public Transform playerPos;
    public float speed;

    private float waitTime; //목표지점 도착 후 기다리는 시간
    public float startWaitTime; // 시작시간

    //private GameObject moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector2 target;
    Vector3 targetPos;
    Vector3 randOffset;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waitTime = startWaitTime;

        //moveSpot = GameObject.FindGameObjectWithTag("MoveSpot");
        randOffset = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
        targetPos = new Vector3(animator.transform.position.x + randOffset.x, animator.transform.position.y + randOffset.y);
        //playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //지정해둔 위치에 랜덤으로 이동한다.
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, targetPos, speed * Time.deltaTime);
        
            if (waitTime <= 0)
            {
                randOffset = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
                targetPos = new Vector3(animator.transform.position.x + randOffset.x, animator.transform.position.y + randOffset.y);
                waitTime = startWaitTime;
                target = new Vector2(targetPos.x - animator.transform.position.x, targetPos.y - animator.transform.position.y).normalized;
                if (target.x > 0)
                {
                    animator.transform.localScale = new Vector3(-1, 1, 1); // 캐릭터 플립
                }
                else if (target.x <= 0)
                {
                    animator.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        


    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
