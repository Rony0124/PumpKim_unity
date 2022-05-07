using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyController_shooter : EnemyBase
{
    public GameObject bullet;
    
 
    private void Awake()
    {
       // AtkTarget = GetComponent<IAtkTarget>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        color = gameObject.GetComponent<SpriteRenderer>().color;
    
    }
  
    // Update is called once per frame
    void Update()
    {
       
        if (isHeadShot)
        {
            currentHP = -1f;
        }
    }
   



}
