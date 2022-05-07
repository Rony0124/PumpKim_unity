using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSet : MonoBehaviour
{
    public Transform skillRoot;
    public TextMeshProUGUI scoreTxt;
    public SkillSlot[] skillSlots;
    public List<Skill> skillList;
   
    public GameObject go;
    public DatabaseMng database;

    public bool activated = true;
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        skillList = new List<Skill>();
      
       
        //RemoveSlots();
    }

    void OnEnable()
    {
        database = FindObjectOfType<DatabaseMng>();
        playerData = FindObjectOfType<PlayerData>();
        skillSlots = skillRoot.GetComponentsInChildren<SkillSlot>();
        //Time.timeScale = 0;
        RemoveSlots();
        scoreTxt.text = StageMng.Instance.currentStage.ToString();

        for (int i = 0; i < playerData.PlayerSkill.Count; i++)
        {
            if (playerData.PlayerSkill[i] != 0)
            {
                GetASkill(i);
                // return;
            }
        }

        ShowSkillSet();
        Time.timeScale = 0;
        activated = false;
    }
    
    private void OnDisable()
    {
        Time.timeScale = 1;
        skillList.Clear();
       
        activated = true;
    }
    void GetASkill(int skillid)
    {
        for (int i = 0; i < database.skillList.Count; i++)//데이터베이스 아이템 검색
        {
            if (skillid == database.skillList[i]._skillid) //데이터베이스 아이템 발견
            {
                skillList.Add(database.skillList[i]); //소지품에 해당 아이템 추가
                //skillList[inventoryItemList.Count - 1].itemCount = _count;
                return;
            }
        }
        Debug.LogError("xxxxx");
    }

    public void ShowSkillSet()
    {
        
        //StopAllCoroutines();
        if(skillList == null)
        {
            for(int i = 0; i < skillSlots.Length; i++)
            {
                skillSlots[i].gameObject.SetActive(false);
            }
            return;
        }
        else
        {
            for (int i = 0; i < skillList.Count; i++)
            {
                skillSlots[i].gameObject.SetActive(true);
                skillSlots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                skillSlots[i].AddSkill(skillList[i]);
            }
        }
      
       
    } //탭활성화
   
    public void RemoveSlots()
    {
        for (int i = 0; i < skillSlots.Length; i++)
        {
            skillSlots[i].RemoveItem();
            skillSlots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            //skillSlots[i].gameObject.SetActive(false);
        }
    }//인벤토리 슬롯 초기화
    public void EndGame()
    {
        PlayerData.Instance.SetScore();
        Application.Quit();
    }
  
}
