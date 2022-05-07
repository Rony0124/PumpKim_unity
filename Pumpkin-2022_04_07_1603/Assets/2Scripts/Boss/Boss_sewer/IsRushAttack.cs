using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsRushAttack : StateMachineBehaviour
{

    public float timer;
    public const float START_TIME = 2f;
    public float minTime;
    public float maxTime;

    private Transform playerPos; //플레이어 위치
   
    Vector3 Dir;

    public float speed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       

        Dir = playerPos.position - animator.transform.position;
        Dir.Normalize();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 currentPos = new Vector2(animator.transform.transform.position.x, animator.transform.transform.position.y);
        Debug.Log(Dir.x);
        if (timer <= 0)
        {
            animator.SetTrigger("IsStop");
           // animator.transform.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            timer -= Time.deltaTime;
           
            if (Dir.x > 0)
            {
                //animator.transform.localScale = new Vector3(-1, 1, 1);
                //animator.transform.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
                animator.transform.GetComponent<SpriteRenderer>().flipX = true;
                //애니메이터 위치에서 타겟 위치로 공격


            }
            else if (Dir.x <= 0)
            {
                animator.transform.GetComponent<SpriteRenderer>().flipX = false;
                //animator.transform.localScale = new Vector3(1, 1, 1);
                //animator.transform.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
                //애니메이터 위치에서 타겟 위치로 공격

            }

        }
        
        animator.transform.transform.position = currentPos + Dir * Time.deltaTime * speed;

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.transform.localScale = new Vector3(1, 1, 1);
        timer = START_TIME;
        animator.transform.GetComponent<SpriteRenderer>().flipX = false;
    }


}
