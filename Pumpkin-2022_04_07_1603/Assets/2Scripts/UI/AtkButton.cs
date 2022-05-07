using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AtkButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    static AtkButton instance;
    public static AtkButton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AtkButton>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("AtkButton");
                    instance = instanceContainer.AddComponent<AtkButton>();

                }
            }
            return instance;
        }
    }
    void Awake() { instance = this; }
    public bool ButtonDown;
    public Vector3 firstPos;
    public float atkspeed;
    public float startTime;
    public float delayTime;
    void Start()
    {
        delayTime = (startTime/PlayerData.Instance.playAtkSpd);
        firstPos = gameObject.transform.position;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.transform.position = eventData.position;
        gameObject.GetComponent<Image>().color = new Color(45 / 255f, 253 / 255f, 248 / 255f, 180 / 255f);
        //버튼을 누른 상태이면 true
        ButtonDown = true;
    }
   
    public void OnPointerUp(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color(0 / 255f, 127 / 255f, 175 / 255f, 123 / 255f);
        gameObject.transform.position = firstPos;
        //연타를 눌러도 시간이 0이상이면 발사x
        //누르고 있는 경우 pointer up 전까지 buttondown 은 true 상태
         ButtonDown = false;
    }

    private void Update()
    {
        //버튼을 누르고 있는 상태이고 쿨타임 시간이 다되었을때
        if (delayTime <= 0 && ButtonDown)
        {
            PlayerTargeting.Instance.Shoot();
           
            delayTime = (startTime / PlayerData.Instance.playAtkSpd);
        }
        //버튼의 상태와 상관없이 시간 순서 계속
        else if(delayTime >0)
        {
            delayTime -= Time.deltaTime;
            
        }
       
    }
    
}
