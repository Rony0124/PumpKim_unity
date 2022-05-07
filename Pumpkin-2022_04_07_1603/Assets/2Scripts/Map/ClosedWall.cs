using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedWall : MonoBehaviour
{
    private CameraMovement theCamera;
    public BoxCollider2D targetBound;
    public GameObject randomSpawn;

    private void Start()
    {
        randomSpawn.SetActive(false);
        StartCoroutine(RandomSpawn());
    }

    IEnumerator RandomSpawn()
    {
        yield return new WaitForSeconds(3.0f);
        randomSpawn.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(GetCamera());
            theCamera = FindObjectOfType<CameraMovement>();
           
        }
    }
    IEnumerator GetCamera()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        yield return new WaitForSeconds(0.1f);
        theCamera.CameraNextRoom(x, y);
        //새로 추가된 부분
        theCamera.SetBound(targetBound);
    }
}
