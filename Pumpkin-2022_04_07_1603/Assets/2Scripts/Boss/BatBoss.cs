using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBoss : MonoBehaviour
{
    BossController bossController;

    private const float STOPPING_DIST = 3f;
    private const float RETREAT_DIST = 2f;

    //IAtkTarget AtkTarget;
    Vector2 targetPos;
    Vector2 pos;
    public int speed;
    private void Awake()
    {
        //AtkTarget = GetComponent<IAtkTarget>();
    }
    // Start is called before the first frame update
    void Start()
    {
        bossController = GetComponent<BossController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (bossController.bossRoom == null) return;
        if (bossController.bossRoom.playerInThisRoom)
        {

            targetPos = PlayerMovement.Instance.GetPos();
            pos = transform.position;
            if (Vector2.Distance(targetPos, pos) > STOPPING_DIST)
            {
                Debug.Log("1");
                Debug.Log(targetPos);
                transform.position = Vector2.MoveTowards(pos, targetPos, speed * Time.deltaTime);
                gameObject.GetComponent<Animator>().SetBool("isAtk", true);
            }
            else if (Vector2.Distance(targetPos, pos) < STOPPING_DIST && Vector2.Distance(targetPos, pos) > RETREAT_DIST)
            {
                Debug.Log("2");
                gameObject.GetComponent<Animator>().SetBool("isAtk", true);

                transform.position = this.transform.position;

            }
            else if (Vector2.Distance(targetPos, pos) < RETREAT_DIST)
            {
                Debug.Log("3");
                transform.position = Vector2.MoveTowards(pos, targetPos, -speed * Time.deltaTime);
                gameObject.GetComponent<Animator>().SetBool("isAtk", false);
            }
        }
        
        


    }
    IEnumerator Wait4Player()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Animator>().SetBool("isAtk", true);
    }
  
}
