using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorials;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tutorials.SetActive(true); //튜토리얼 창 생성
        }
    }

            private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            tutorials.SetActive(false); //방에서 나갈 때 튜토리얼 창 없앰
        }
    }
}
