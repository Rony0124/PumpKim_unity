using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour_LarvaBoss : StateMachineBehaviour
{
    //public Transform playerPos;
    public float speed;

    private float waitTime; //목표지점 도착 후 기다리는 시간
    public float startWaitTime; // 시작시간

    private GameObject moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector2 target;
    //public float speed;
    public float time;
    public const float START_TIME = 1.5f;
    public Vector3 playerPos;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waitTime = startWaitTime;

        moveSpot = GameObject.FindGameObjectWithTag("MoveSpot");

        //playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 Dir = (PlayerMovement.Instance.GetPos() - animator.transform.position).normalized;
       
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
