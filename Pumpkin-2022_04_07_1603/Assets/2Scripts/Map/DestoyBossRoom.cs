using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyBossRoom : MonoBehaviour
{
    public GameObject cleanRoom;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //보스 리스폰 되기 전에 방을 청소한다.
        if (other.CompareTag("Enemy"))
        {
            
            RoomCondition.Instance.MonsterListInRoom.Remove(other.gameObject);
        }
        Destroy(other.gameObject);
        StartCoroutine(Clean());

    }



    IEnumerator Clean()
    {
        //1초 후 자기 자신삭제
        yield return new WaitForSeconds(1f);
        //Debug.Log(cleanRoom.name);
        Destroy(cleanRoom);
    }
}
