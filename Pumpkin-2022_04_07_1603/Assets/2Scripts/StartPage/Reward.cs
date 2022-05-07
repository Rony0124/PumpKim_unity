using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    public enum RewardType
    {
        RandPick, Achieve
    }
    public RewardType rewardType;
    //public RecordVO record;
    public int Requirement_ep, Requirement_stage;
    public int itemId;
    public RectTransform useBtn;
    public Text txtName;
    public Text txtDescription;
    public bool isActivated = true;
    //public bool isPicked = false;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(rewardType == RewardType.Achieve)
        {
            if (PlayerData.Instance.records[Requirement_ep].bestStage > Requirement_stage && isActivated)
            {
                isActivated = false;
                PlayerData.Instance.ownedItems.Add(itemId);
                PlayerData.Instance.ownedItems = PlayerData.Instance.ownedItems.Distinct().ToList();
                useBtn.gameObject.GetComponent<Button>().interactable = true;
                txtName.color = new Color(0f, 0f, 0f);
                txtDescription.color = new Color(0f, 0f, 0f);
                for (int i = 0; i < DatabaseMng.instance.weaponList.Count; i++)
                {
                    if (DatabaseMng.instance.weaponList[i]._weaponid == itemId)
                    {
                        txtName.text = DatabaseMng.instance.weaponList[i]._name;
                        //txtDescription.text = DatabaseMng.instance.weaponList[i]._func;
                    }
                }
            }
        } else if (rewardType == RewardType.RandPick)
        {
            if (isActivated)
            {
                for(int i = 0; i < PlayerData.Instance.ownedItems.Count; i++)
                {
                    if(PlayerData.Instance.ownedItems[i] == itemId)
                    {
                        isActivated = false;
                        useBtn.gameObject.GetComponent<Button>().interactable = true;
                        txtName.color = new Color(1f, 1f, 1f);
                        txtDescription.color = new Color(1f, 1f, 1f);
                        for (int j = 0; j < DatabaseMng.instance.weaponList.Count; j++)
                        {
                            if (DatabaseMng.instance.weaponList[j]._weaponid == itemId)
                            {
                                txtName.text = DatabaseMng.instance.weaponList[j]._name;
                                txtDescription.gameObject.SetActive(true);
                                txtDescription.text = DatabaseMng.instance.weaponList[j]._func;
                            }
                        }
                        return;
                    }
                }
                
        /*        PlayerData.Instance.ownedItems.Add(itemId);
                PlayerData.Instance.ownedItems = PlayerData.Instance.ownedItems.Distinct().ToList();*/
                
            }
        }
        
       
    }
}
