using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    
    //열쇠
    public GameObject goldKey;

    public GameObject closedRoom;
    public List<GameObject> rooms;


    public float distoyWaitTime; 
    public float waitTime; //보스 나오기전 대기 시간
    private bool spawnedBoss;

    public GameObject portal;
    public GameObject destroyerBossroom;

    private void Update()
    {

        if (waitTime <= 0 && spawnedBoss == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    Instantiate(destroyerBossroom, rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                    StartCoroutine(SpawnBoss(i));
                    

                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
    IEnumerator SpawnBoss(int i)
    {
        //Debug.Log("코루틴 실험1");
        yield return new WaitForSeconds(2f);
        //Debug.Log(rooms[i].transform.position);
        Instantiate(portal, rooms[i].transform.position, Quaternion.identity).transform.parent = rooms[i].transform;


        //열쇠 랜덤 생성
        int j = Random.Range(1, rooms.Count - 1);
        //Debug.Log(rooms[i].transform.localPosition);

        Instantiate(goldKey, rooms[j].transform.position, Quaternion.identity).transform.parent = rooms[j].transform;
    }
}
