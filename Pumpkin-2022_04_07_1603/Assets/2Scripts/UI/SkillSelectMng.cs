using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectMng : MonoBehaviour
{
    public GameObject[] SlotSkillObject; //스킬아이콘이 그려지고 움직일 object
    public TextMeshProUGUI txt;
    public Button[] Slot; //버튼

    public Sprite[] SkillSprite;//스킬 아이콘으로 쓸 스프라이트
   
    public List<Image> slotSprite = new List<Image>();
  
    //랜덤뽑기 리스트
    public List<int> StartList = new List<int>();
    //당첨된 스킬의 인덱스저장
    public List<int> ResultIndexList = new List<int>();
    //슬롯위 올라갈 아이템 갯수
    int ItemCnt = 3;
   
    private void OnEnable()
    {
        RanGen();
        txt.text = "Current Stage: " + StageMng.Instance.currentStage.ToString();
    }
    private void OnDisable()
    {
        txt.text = "Current Stage: ";
        StartList.Clear();
        ResultIndexList.Clear();
    }
  
    void RanGen()
    {
        //Debug.Log(Slot.Length);
        for (int i = 0; i < ItemCnt * Slot.Length; i++)
        {
            StartList.Add(i);

        }
        for (int i = 0; i < Slot.Length; i++)
        {
            int randomIndex = Random.Range(0, StartList.Count);
            ResultIndexList.Add(StartList[randomIndex]);
            slotSprite[i].sprite = SkillSprite[StartList[randomIndex]];
            for(int j =0; j < DatabaseMng.instance.skillList.Count; j++)
            {
                if(DatabaseMng.instance.skillList[j]._skillid == StartList[randomIndex] + 1)
                {
                    slotSprite[i].transform.GetChild(1).GetComponent<Text>().text = DatabaseMng.instance.skillList[j]._name;

                    slotSprite[i].transform.GetChild(2).GetComponent<Text>().text ="Lv. "+ PlayerData.Instance.PlayerSkill[j].ToString();
                    if(PlayerData.Instance.PlayerSkill[j] >= 1)
                    {
                        if(j <= 4)
                        {
                            slotSprite[i].transform.GetChild(0).GetComponent<Text>().text = DatabaseMng.instance.skillList[j]._content;
                        }else
                        {
                            slotSprite[i].transform.GetChild(0).GetComponent<Text>().text = DatabaseMng.instance.skillList[j]._func;
                        }
                    }
                    else
                    {
                        slotSprite[i].transform.GetChild(0).GetComponent<Text>().text = DatabaseMng.instance.skillList[j]._func;
                    }
                    //return;
                }
            }
            
            //중복방지
            StartList.RemoveAt(randomIndex);
        }
      
                
    }
    public void ClickBtn(int index)
    {
        PlayerData.Instance.PlayerSkill[ResultIndexList[index] + 1]++;
        if (ResultIndexList[index] + 1 == 2)
        {
            PlayerTargeting.Instance.bulletindex++;
        }
        PlayerData.Instance.CheckSkill(ResultIndexList[index] + 1);
        gameObject.SetActive(false);
        //playerdata에서 스킬에 따라 dmg 조절
        
    }
}
