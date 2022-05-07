using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpSlider : MonoBehaviour
{
    public GameObject boss;
    //private float bossHp;
    Slider healthBar; // 체력게이지
    // Start is called before the first frame update
    void Awake()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = boss.GetComponent<BossController>().maxHP;
        healthBar.value = healthBar.maxValue;
       
    }

    // Update is called once per frame
    void Update()
    {

        healthBar.value = BossController.Instance.currentHP;
    }
}
