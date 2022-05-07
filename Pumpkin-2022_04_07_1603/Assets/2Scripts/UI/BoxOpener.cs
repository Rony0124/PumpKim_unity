using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class BoxOpener : MonoBehaviour
{
    Vector2 pos;
    public GameObject[] itmeDrop;
    bool isclicked;
    // Start is called before the first frame update
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (AtkButton.Instance.ButtonDown && !isclicked)
            {
                Debug.Log("Box is open");
                pos = new Vector2(transform.position.x, transform.position.y);
                GetComponent<Animator>().SetTrigger("Open");
                Invoke("Boxdestroy", 0.5f);
                isclicked = true;
            }
        }
    }
    void Boxdestroy()
    {
        int num = Random.Range(0, itmeDrop.Length);
        GameObject temp = Instantiate(itmeDrop[num], pos + new Vector2(0, 0.3f), Quaternion.identity);
        temp.transform.DOMove(pos + new Vector2(0, 1f), 1f);
        //StartCoroutine(ScaleHandler(temp));
      
        //gameObject.SetActive(false);
    }
    



}
