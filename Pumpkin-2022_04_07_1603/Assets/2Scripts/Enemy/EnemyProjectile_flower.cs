using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile_flower : MonoBehaviour
{
    public float speed;

    void Start()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        StartCoroutine(DestroyProjectile());
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
