using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    public GameObject targetObj; //이동할 오브젝트


    public GameObject toObj; //이동할 곳 오브젝트

    private bool isKey = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetObj = collision.gameObject; //닿는 순간 플레이어에게 위치값 받아온다.
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if(collision.CompareTag("Player"))
        {
            for (int i = 0; i < PlayerData.Instance.ItemList.Count; i++)
            {
                if (PlayerData.Instance.ItemList[i] == 10002)
                {
                    StartCoroutine(TeleportRoutine()); //접촉해 있으면 텔레포트 한다.
                    PlayerData.Instance.ItemList.RemoveAt(i);
                    UIController.Instance.ItemSlotClear();
                    isKey = true;
                    return;
                }
                
            }
            //Debug.Log("zzzz" + PlayerData.Instance.ItemList.Exists(x => x == 1002));
            if (!isKey)
            {
                collision.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                StartCoroutine(EnableWords(collision.transform.GetChild(0).GetChild(1).gameObject));
            }
            
            
        }
    }
    IEnumerator EnableWords(GameObject GO)
    {
        yield return new WaitForSeconds(3f);
        GO.SetActive(false);
    }

    IEnumerator TeleportRoutine()
    {
        yield return null;
        targetObj.transform.position = toObj.transform.position; //이동
    
    }
  
}
