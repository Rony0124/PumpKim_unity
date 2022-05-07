using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAtk : MonoBehaviour
{
    public float time;
    void Start()
    {
        StartCoroutine(SelfDestruction());
    }

    IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
