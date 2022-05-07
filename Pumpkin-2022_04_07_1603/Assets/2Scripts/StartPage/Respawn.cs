using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    static Respawn instance;
    public static Respawn Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Respawn>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("Respawn");
                    instance = instanceContainer.AddComponent<Respawn>();

                }
            }
            return instance;
        }
    }
    void Awake() { instance = this; }
    public GameObject[] charPrefabs;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        //Player = Instantiate();
        Player = Instantiate(charPrefabs[(int)DataMng.instance.currentCharacter]);
        Player.transform.position = transform.position;
        //StartCoroutine(PlayerRespawn());

        Debug.Log("플레이어 리스폰 위치 = " + Player.transform.localPosition);
    }

    /*IEnumerator PlayerRespawn()
    {
        yield return new WaitForSeconds(0.3f);
        Player = Instantiate(charPrefabs[(int)DataMng.instance.currentCharacter]);
        Player.transform.position = transform.position;
    }*/
  
}
