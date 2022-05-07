using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomCondition : MonoBehaviour
{
    static BossRoomCondition instance;
    public static BossRoomCondition Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BossRoomCondition>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("BossRoomCondition");
                    instance = instanceContainer.AddComponent<BossRoomCondition>();

                }
            }
            return instance;
        }

    }
    void Awake() { instance = this; }
    public List<GameObject> MonsterListInRoom = new List<GameObject>();
    public bool playerInThisRoom;
    public bool isClearRoom;
    public bool isBossInRoom;
    private CameraMovement theCamera;
    public GameObject nextStage;
    //새로 추가 카메라 이동
    public BoxCollider2D targetBound;
    public GameObject SkillSelector;
    Vector3 nextRoomPos;

    public GameObject bossHpslider;
    public GameObject[] boss;
    public GameObject bossSpwanPoint;
    public List<GameObject> monsters = new List<GameObject>();
    public GameObject currentBoss;
    public bool isBossDead;
    void Start()
    {
        // nextStage = GameObject.FindGameObjectWithTag("NextStage");
        theCamera = FindObjectOfType<CameraMovement>();
       // gate = gameObject.transform.Find("GateMng").GetComponent<GateMng>();
        // Debug.Log(theCamera.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInThisRoom)
        {
            if (MonsterListInRoom.Count <= 0 && !isClearRoom)
            {
                
                if (isBossInRoom)
                {
                    //Debug.Log("success");
                    StartCoroutine(GenerateNextRoomGate());
                    SkillSelector.SetActive(true);
                    isClearRoom = true;
                }
            }
            if (currentBoss == null)
            {
               // Debug.Log("isdeqa");
                foreach(GameObject monster in monsters)
                {
                    if (monster != null) monster.GetComponent<EnemyController>().Damgage(10000f);

                }
            }
        }
    }
    IEnumerator GenerateNextRoomGate()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(nextStage, nextRoomPos, Quaternion.identity);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            playerInThisRoom = true;
            PlayerTargeting.Instance.MonsterList = MonsterListInRoom;
            //플레이어가 방에 들어왔을경우

            Debug.Log("entered the room");
            
            float x = transform.position.x;
            float y = transform.position.y;
            theCamera.theCamera.orthographicSize += 1.2f;
            theCamera.CameraNextRoom(x, y);
            //새로 추가된 부분
          
            theCamera.SetBound(targetBound);
             
            int index = Random.Range(0, boss.Length);
            Instantiate(boss[index], bossSpwanPoint.transform.position, Quaternion.identity).transform.parent = gameObject.transform;
            bossHpslider.SetActive(true);
            UIController.Instance.BossRoomEnter();

        }
      
        if (other.CompareTag("Boss"))
        {
            isBossInRoom = true;
            Debug.Log("boss is here");
            MonsterListInRoom.Add(other.gameObject);
            currentBoss = other.gameObject;
            nextRoomPos = bossSpwanPoint.transform.position;
        }
        if (other.CompareTag("Enemy"))
        {
            monsters.Add(other.gameObject);
            MonsterListInRoom.Add(other.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            /// PlayerTargeting.Instance.MonsterList.Clear();
            playerInThisRoom = false;
            //isBossInRoom = false;
        }
    }
    


}
