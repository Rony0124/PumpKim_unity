using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public RoomTemplates roomTemplates;
    List<GameObject> rooms = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
        roomTemplates = FindObjectOfType<RoomTemplates>();
        rooms = roomTemplates.rooms;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int index = Random.Range(0, rooms.Count);
            collision.transform.position = rooms[index].transform.position;
        }
    }

    

}
