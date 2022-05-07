using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawn : MonoBehaviour
{
    public GameObject bossHpslider;
    public GameObject boss;
    public GameObject bossSpwanPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Instantiate(boss, bossSpwanPoint.transform.position, Quaternion.identity).transform.parent = bossSpwanPoint.transform;
            bossHpslider.SetActive(true);
        }
    }
}
