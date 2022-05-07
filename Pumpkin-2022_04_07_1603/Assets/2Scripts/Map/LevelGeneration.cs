using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] objects;
    public Transform Room;
    private void Start()
    {

        Room = gameObject.transform.parent.parent;

        int rand = Random.Range(0, objects.Length);

        if (objects.Length == 0)
        {
            //Debug.Log("아무것도 없음");
        }
        else
        {
            //instantiate할때, 스폰포인트의 자식개체로 들어간다
            Instantiate(objects[rand], transform.position, Quaternion.identity).transform.parent = Room;
        }
        
    }
}
