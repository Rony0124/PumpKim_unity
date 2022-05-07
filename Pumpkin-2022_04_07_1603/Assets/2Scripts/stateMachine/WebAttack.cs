using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAttack : StateMachineBehaviour
{
    public float timer;
    public float fireRate;
    private const int BULLETS_TO_FIRE = 10;
    private const float FIRE_RATE = 0.7f;
    public int bulletsFired;
   
    private Transform enemyTarget;
    public bool timeUp;
    Bullet_spider shot;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 2f;
        bulletsFired = 0;
        enemyTarget = GameObject.FindGameObjectWithTag ("Player").transform;
        animator.SetBool("IsAttack", true);
        shot = animator.transform.GetComponent<Bullet_spider>();
        timeUp = false;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timeUp)
        {
            animator.SetTrigger("IsStop");
        }
        else if(!timeUp)
        {
            //timer -= Time.deltaTime;
            fireRate -= Time.deltaTime;
            if (fireRate <= 0f)
            {
                TestContinueShooting();
            }
            
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsAttack", false);
    }
    private void ShootTarget()
    {
        enemyTarget = GameObject.FindGameObjectWithTag("Player").transform;
        //if (enemyTarget != null)
        //{
            //Sound_Manager.PlaySound(Sound_Manager.Sound.Rifle_Fire, GetPosition());
            //Vector3 targetPosition = enemyTarget.position;

            timeUp = false;
            fireRate = FIRE_RATE;
            Debug.Log("shoot");
            shot.ShootTarget();
            //UtilsClass.GetRandomDir()
            //targetPosition += UnityEngine.Random.Range(-5f, 15f);
            //aimAnims.ShootTarget(targetPosition, () => { });


       // }
    }
    private void TestContinueShooting()
    {
        enemyTarget = GameObject.FindGameObjectWithTag("Player").transform;
        if (bulletsFired < BULLETS_TO_FIRE)
        {
            bulletsFired++;
            ShootTarget();
        }
        else
        {
            timeUp = true;
            //SetStateNormal();
        }
    }

    
    
}
