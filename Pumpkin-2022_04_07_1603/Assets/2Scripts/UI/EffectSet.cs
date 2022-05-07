using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSet : MonoBehaviour
{
    static EffectSet instance;
    public static EffectSet Instance
    {
        get { return instance; }
    }
    void Awake() { instance = this; }
    [Header("Enemy")]
    //public GameObject DuckAtkEffect;
    //public GameObject DuckDmgEffect;
    public GameObject EnemyDmgTxt;

    [Header("Player")]
    public GameObject PlayerAtkEffect;
    public GameObject PlayerAtkEffect1;
   
    //public GameObject PlayerLevelUpEffect;


}
