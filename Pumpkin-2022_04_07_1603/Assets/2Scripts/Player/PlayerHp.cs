using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{

  
    static PlayerHp instance;
    public static PlayerHp Instance
    {
        get { return instance; }
    }
    void Awake() { instance = this; }
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject Hps;

    private void Start()
    {
        Hps = GameObject.FindGameObjectWithTag("Heart");
        hearts = Hps.GetComponentsInChildren<Image>();
    }
    void Update()
    {
        
        if ( health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = fullHeart;
            hearts[i].color = new Color(1f, 1f, 1f, 1f);
            if (i >= health)
            {
                hearts[i].sprite = emptyHeart;
                hearts[i].color = new Color(1f, 1f, 1f, 0);
            } 
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        
    }
    private void FixedUpdate()
    {
        health = PlayerData.Instance.currentHp;
        numOfHearts = PlayerData.Instance.maxHp;
    }

}
