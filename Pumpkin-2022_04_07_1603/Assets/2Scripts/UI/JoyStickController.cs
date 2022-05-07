using System.Collections;
using System.Collections.Generic;
using System.Net;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour
{
    static JoyStickController instance;
    public static JoyStickController Instance
    {
        get {
            if (instance == null)
            {
                instance = FindObjectOfType<JoyStickController>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("JoyStickController");
                    instance = instanceContainer.AddComponent<JoyStickController>();
                    
                }
            }
            return instance;
        }
    }
    void Awake() { instance = this; }
    public GameObject Player;
    public GameObject bgStick;
    public GameObject Stick;         // 조이스틱.
    public float movespeed;
   
    public Animator playerAnim;
    public GameObject Aim;
  
    private Vector3 StickFirstPos;  // 조이스틱의 처음 위치.
    public Vector3 JoyStickfirstPos;
    public Vector3 JoyVec;         // 조이스틱의 벡터(방향)
    private float Radius;           // 조이스틱 배경의 반 지름.
 

    void Start()
    {
        Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        JoyStickfirstPos = Stick.transform.position;

        // 캔버스 크기에대한 반지름 조절.
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;
        Player = GameObject.FindGameObjectWithTag("Player");
        Aim = GameObject.FindGameObjectWithTag("Aim");
        playerAnim = Player.transform.GetComponent<Animator>();
    }

    private void Update()
    {
        
        float absX = Mathf.Abs(JoyVec.x);
        float absY = Mathf.Abs(JoyVec.y);

        Aim.transform.position = Player.transform.position + JoyVec * 1.0f;
        
        if (absX > absY && JoyVec.x > 0)
        {
            Player.transform.GetComponent<SpriteRenderer>().flipX = false;
            playerAnim.SetBool("Walk", true);
            playerAnim.SetBool("LookTop", false);
            playerAnim.SetBool("Walk_front", false);
        } else if (absX > absY && JoyVec.x < 0)
        {
            Player.transform.GetComponent<SpriteRenderer>().flipX = true;
            playerAnim.SetBool("Walk", true);
            playerAnim.SetBool("LookTop", false);
            playerAnim.SetBool("Walk_front", false);

        } else if (absX < absY && JoyVec.y > 0)
        {
            Player.transform.GetComponent<SpriteRenderer>().flipX = false;
            playerAnim.SetBool("Walk", false);
            playerAnim.SetBool("LookTop", true);
            playerAnim.SetBool("Walk_front", false);
        }
        else if (absX < absY && JoyVec.y < 0)
        {
            Player.transform.GetComponent<SpriteRenderer>().flipX = false;
            playerAnim.SetBool("Walk", false);
            playerAnim.SetBool("LookTop", false);
            playerAnim.SetBool("Walk_front", true);
        }

    }
    public void OnPointerDown(BaseEventData _data)
    {
        PointerEventData Data = _data as PointerEventData;
        Vector3 Pos = Data.position;
       // bgStick.GetComponent<Image>().color = new Color(1f,1f,1f,1f);
        bgStick.GetComponent<Image>().color = new Color(45 / 255f, 253 / 255f, 248 / 255f, 255 / 255f);
        Stick.GetComponent<Image>().color = new Color(45 / 255f, 253 / 255f, 248 / 255f, 255 / 255f);

        //터치가 되면 그 위치에 조이스틱을 위치시킨다
        //처음터치ㄱ된 위치를 저장시키기
        bgStick.transform.position = Pos;
        Stick.transform.position = Pos;

        StickFirstPos = Pos;
        
    }

    // 드래그
    public void Drag(BaseEventData _Data)
    {

        //MoveFlag = true;
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        // 조이스틱을 이동시킬 방향을 구함.(오른쪽,왼쪽,위,아래)
        JoyVec = (Pos - StickFirstPos).normalized;

        // 조이스틱의 처음 위치와 현재 내가 터치하고있는 위치의 거리를 구한다.
        float Dis = Vector3.Distance(StickFirstPos, Pos);

        // 거리가 반지름보다 작으면 조이스틱을 현재 터치하고 있는곳으로 이동. 
        if (Dis < Radius)
            Stick.transform.position = StickFirstPos + JoyVec * Dis;
        // 거리가 반지름보다 커지면 조이스틱을 반지름의 크기만큼만 이동.
        else
            Stick.transform.position = StickFirstPos + JoyVec * Radius;

        //Aim rotation
        float angle = -Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg ;
        Aim.transform.rotation = Quaternion.Euler(0f,0f,angle);
     
    }

    // 드래그 끝.
    public void DragEnd()
    {
        bgStick.transform.position = JoyStickfirstPos;
        Stick.transform.position = JoyStickfirstPos; // 스틱을 원래의 위치로.
        JoyVec = Vector3.zero;          // 방향을 0으로.
        playerAnim.SetBool("Walk", false);
        playerAnim.SetBool("LookTop", false);
        playerAnim.SetBool("Walk_front", false);
        Player.transform.GetComponent<SpriteRenderer>().flipX = false;
        bgStick.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        Stick.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        //MoveFlag = false;
       // MultipleTouch.Instance.isJoyStick = false;
    }
    

    
    
}
