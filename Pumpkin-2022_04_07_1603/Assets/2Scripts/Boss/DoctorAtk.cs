using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoctorAtk : MonoBehaviour, IAtkTarget
{
    private const float STOPPING_DIST = 3f;
    private const float RETREAT_DIST = 2f;

    private Vector3 targetPos;
    private Vector3 pos;
    private int speed;

    public GameObject ax;
    public GameObject electricShock;
    Vector2 dir;
    BossController bossController;
    void Start()
    {
        bossController = GetComponent<BossController>();
    }
    void Update()
    {
        targetPos = PlayerMovement.Instance.GetPos();
        dir = (targetPos - transform.position).normalized;
        //Debug.Log(dir);
        if (bossController.bossRoom == null) return;
        if (bossController.bossRoom.playerInThisRoom && !bossController.isDead)
        {
            StartCoroutine(Wait4Player());
            targetPos = PlayerMovement.Instance.GetPos();
        }

    }
    IEnumerator Wait4Player()
    {
        yield return new WaitForSeconds(1f);
        AtkTarget();

    }
    void SetStop()
    {
        GetComponent<Animator>().SetTrigger("isStop");
    }
    void Atk()
    {
        double angle = Math.Atan2(dir.y, dir.x);

        angle = angle * 180 / Math.PI;
        Math.Round((decimal)angle, 3);
        Instantiate(ax, PlayerMovement.Instance.GetPos(), Quaternion.Euler(0, 0, (float)angle + 180f));
    }
    void ElectrickShock()
    {
        Instantiate(electricShock, transform.position, Quaternion.identity);
    }
    public void AtkTarget()
    {
        gameObject.GetComponent<Animator>().SetBool("isAtk", true);
    }
    public void AtkTarget(Vector3 _targetPos, Vector3 _pos, int _speed)
    {
        targetPos = _targetPos;
        pos = _pos;
        speed = _speed;
    }
}
