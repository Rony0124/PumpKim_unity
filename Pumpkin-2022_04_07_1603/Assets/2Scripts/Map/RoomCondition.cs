using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCondition : MonoBehaviour
{



    static RoomCondition instance;
    public static RoomCondition Instance 
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RoomCondition>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("RoomCondition");
                    instance = instanceContainer.AddComponent<RoomCondition>();

                }
            }
            return instance;
        }

    }
   
    public List<GameObject> MonsterListInRoom = new List<GameObject>();
    public bool playerInThisRoom;
    public bool isClearRoom;
    public bool isBossInRoom;
    private CameraMovement theCamera;
    private GateMng gate;
    public bool isDead;
    public GameObject nextStage;
    //새로 추가 카메라 이동
    public BoxCollider2D targetBound;



    //public GameObject moveSpot; // 애벌레가 돌아다니는 스폿

    void Awake(){ instance = this; }
    // Start is called before the first frame update
    void Start()
    {
       // nextStage = GameObject.FindGameObjectWithTag("NextStage");
        theCamera = FindObjectOfType<CameraMovement>();
        gate = GetComponentInChildren<GateMng>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInThisRoom)
        {
           
            if (MonsterListInRoom.Count <= 0 && !isClearRoom)
            {
                isClearRoom = true;
            
                for (int i = 0; i < gate.openGate.Length; i++)
                {
                    gate.openGate[i].SetActive(true);
                    gate.closeGate[i].SetActive(false);
                }
            }
        }
    }
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            playerInThisRoom = true;
            //방안 몬스터 플레이어타게팅에 추가
            PlayerTargeting.Instance.MonsterList = MonsterListInRoom;
            //플레이어가 방에 들어왔을경우

            //moveSpot.gameObject.SetActive(true);//애벌레 움직임 추가

            Debug.Log("entered the room");
            
            float x = transform.position.x;
            float y = transform.position.y;
            theCamera.CameraNextRoom(x, y);
            //새로 추가된 부분
            theCamera.SetBound(targetBound);

            //Debug.Log(gate.openGate.Length);
            if (!isClearRoom)
            {
                for (int i = 0; i < gate.openGate.Length; i++)
                {
                    gate.openGate[i].SetActive(false);
                    gate.closeGate[i].SetActive(true);
                }
            }
        }
        if ( other.CompareTag("Enemy"))
        {
            
            MonsterListInRoom.Add(other.gameObject);
            
        }
        if (other.CompareTag("Boss"))
        {
            isBossInRoom = true;
            Debug.Log("boss is here");

            MonsterListInRoom.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //moveSpot.gameObject.SetActive(false); //새로추가 애벌레 무브스팟 다시 비활성화

            playerInThisRoom = false;
        }
    }

   
}
