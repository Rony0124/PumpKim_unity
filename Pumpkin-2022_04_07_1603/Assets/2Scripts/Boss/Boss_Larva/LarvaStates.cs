using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LarvaStates : MonoBehaviour
{
    public enum State
    {
        stage0, stage1, stage2, stage3, stage4, stage5, stage6
    }
    public State stage;
    public State prevStage;
    private float phase;
    public GameObject[] bodies;
    public GameObject Larva;
    public int larvaSpawn;
    public bool isPhased;
    void Update()
    {
        if (BossController.Instance.isHit)
        {
            prevStage = stage;
            SetState();
           
            if (prevStage != stage) Case();
            
            BossController.Instance.isHit = false;
        }
        
       

    }
    void Case()
    {
        switch (stage)
        {
            case State.stage1:
                Destroy(bodies[bodies.Length - 1]);
                StartCoroutine(SpawnLarva());
                
                break;
            case State.stage2:
                Destroy(bodies[bodies.Length - 1]);
                Destroy(bodies[bodies.Length - 2]);
                StartCoroutine(SpawnLarva());
                //isPhased = true;
                break;
            case State.stage3:
                Destroy(bodies[bodies.Length - 1]);
                Destroy(bodies[bodies.Length - 2]);
                Destroy(bodies[bodies.Length - 3]);
                StartCoroutine(SpawnLarva());
                //isPhased = true;
                break;
            case State.stage4:
                Destroy(bodies[bodies.Length - 1]);
                Destroy(bodies[bodies.Length - 2]);
                Destroy(bodies[bodies.Length - 3]);
                Destroy(bodies[bodies.Length - 4]);
                StartCoroutine(SpawnLarva());
                //isPhased = true;
                break;
            case State.stage5:
                Destroy(bodies[bodies.Length - 1]);
                Destroy(bodies[bodies.Length - 2]);
                Destroy(bodies[bodies.Length - 3]);
                Destroy(bodies[bodies.Length - 4]);
                Destroy(bodies[bodies.Length - 5]);
                StartCoroutine(SpawnLarva());
                //isPhased = true;
                break;
            case State.stage6:
                Destroy(bodies[bodies.Length - 1]);
                Destroy(bodies[bodies.Length - 2]);
                Destroy(bodies[bodies.Length - 3]);
                Destroy(bodies[bodies.Length - 4]);
                Destroy(bodies[bodies.Length - 5]);
                Destroy(bodies[bodies.Length - 6]);
                StartCoroutine(SpawnLarva());
                //isPhased = true;
                break;
        }
    }
    
    IEnumerator SpawnLarva()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < larvaSpawn; i++)
        {
            GameObject temp = Instantiate(Larva);
            temp.transform.parent = transform.parent;
           
            temp.transform.GetComponent<EnemyController>().maxHP *= 0.7f;
            temp.transform.GetComponent<EnemyController>().currentHP = temp.transform.GetComponent<EnemyController>().maxHP;
            temp.transform.GetComponent<Animator>().SetBool("isAtk", true);

            float deg = Random.Range(-360f, 360f);

            temp.transform.position = new Vector2(this.transform.GetChild(0).position.x + 1f * Mathf.Sin(deg), this.transform.GetChild(0).position.y + 1f * Mathf.Cos(deg));
            yield return new WaitForSeconds(0.2f);
        }
        
       

    }
    void SetState()
    {
        phase = (float)(BossController.Instance.currentHP / BossController.Instance.maxHP);
        if(phase <= 0.9)
        {
            stage = State.stage1;
            
        }if(phase <= 0.8)
        {
            stage = State.stage2;
           
        }
       if (phase <= 0.7)
        {
            stage = State.stage3;
           
        }
        if (phase <= 0.6)
        {
            stage = State.stage4;
           
        }
        if (phase <= 0.5)
        {
            stage = State.stage5;
           
        }
        if (phase <= 0.2)
        {
            stage = State.stage6;
          
        }
    }
}
