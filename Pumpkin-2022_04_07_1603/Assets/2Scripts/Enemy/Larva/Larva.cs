using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larva : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            StartCoroutine(RegenMove());
        }
    }
    IEnumerator RegenMove()
    {
        gameObject.GetComponent<Animator>().SetBool("isAtk", false);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Animator>().SetBool("isAtk", true);
    }

}
