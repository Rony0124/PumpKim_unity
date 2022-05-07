using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBehavior : StateMachineBehaviour
{

    public float timer; //얼마나 재생시킬것인지 시간
    public float minTime;
    public float maxTime;
    public float speed; //플레이어가 적에게 다가가는 속도
    public float stoppingDistance; //캐릭터가 일정이상 가까우면 멈춘다.

    private Transform playerPos; //플레이어 위치

    private Vector2 target; // dirX ,dirY 연결

    private Animator animator;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = new Vector2(playerPos.transform.position.x - animator.transform.position.x, playerPos.transform.position.y - animator.transform.position.y).normalized;

        animator.SetFloat("DirX", target.x);
        animator.SetFloat("DirY", target.y);

        if (timer <= 0)
        {
            animator.SetTrigger("IsStop");
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (Vector2.Distance(animator.transform.position, playerPos.position) > stoppingDistance)
        {
            //플레이어를 향해 이동
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);
        }



    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


    }



}
