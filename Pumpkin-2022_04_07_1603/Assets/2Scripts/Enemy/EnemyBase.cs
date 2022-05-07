//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] public AudioClip buttonCliclSFX;
    //stats
    public float maxHP;
    public float currentHP;
    public float speed = 1f;
   
    public Color color;
    
    public Vector3 playerPos;

    public IAtkTarget AtkTarget;

    public bool isDead;
    public bool isHeadShot;
    public bool isHit;
    public const float WAIT4PLAYER = 1f;
    void Awake()
    {
        float x = Mathf.Pow(1.2f, (StageMng.Instance.currentStage-1));
        if(StageMng.Instance.currentEp == 2)
        {
            maxHP *= 1.3f;    
        } else if (StageMng.Instance.currentEp == 3)
        {
            maxHP *= 4f;
        }

        maxHP *= x;
        currentHP = maxHP;

        color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        GetComponent<SpriteRenderer>().color = color;
    }

    public void Damgage(float dmg)
    {
        currentHP -= dmg;
        isHit = true;
        if (currentHP > 0)
        {
            StartCoroutine(FadeOutDmg());
        }
        if(currentHP < 0)
        {
            currentHP = 0;
        }
        if(currentHP <= 0)
        {
            isDead = true;
            PlayerTargeting.Instance.MonsterList.Remove(transform.gameObject);
            PlayerTargeting.Instance.TargetIndex = -1;
            RoomCondition.Instance.MonsterListInRoom.Remove(transform.gameObject);
            gameObject.layer = 17;
            gameObject.GetComponent<Animator>().SetTrigger("isDead");
            StartCoroutine(RemoveEnemy());
        }
    }
    IEnumerator FadeOutDmg()
    {

        float a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.9f, 0.02f, 0.02f, a);
        yield return new WaitForSeconds(0.3f);
        while (a >= 0.8f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.9f, 0.02f, 0.02f, a);
            a -= 0.01f;
            yield return null;
        }
        gameObject.GetComponent<SpriteRenderer>().color = color;


    }
    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(transform.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Projectile"))
        {
            AudioMng.Instance.PlaySFX(buttonCliclSFX);
            GameObject DmgTextClone = Instantiate(EffectSet.Instance.EnemyDmgTxt, collision.transform.position, Quaternion.identity);
            int dmg = PlayerData.Instance.playDamage;

            if (PlayerData.Instance.PlayerSkill[4] != 0)
            {
                //헤드샷 확률
                if (Random.value < 0.05)
                {
                    isHeadShot = true;
                }
            }
            //크리티컬 확률
            if (Random.value < (1 - PlayerData.Instance.playCritical))
            {
                DmgTextClone.GetComponent<DmgTxt>().DisplayDamage(dmg, false);
            }
            else
            {
                dmg *= 2;
                DmgTextClone.GetComponent<DmgTxt>().DisplayDamage(dmg, true);
            }
            Damgage(dmg);

        }
    }

}
