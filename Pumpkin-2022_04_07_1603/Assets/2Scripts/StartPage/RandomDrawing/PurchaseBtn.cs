using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseBtn : MonoBehaviour
{
    public static PurchaseBtn instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;

    }
    public ResourceType resourceType;
    public int price;
    public Text text;
    public GameObject ChestGruop;
    public bool isShort;
    public string alert;
    [SerializeField] public AudioClip audioClip;
    [SerializeField] public AudioClip buttonCliclSFX;
    public string defalutText;
    private void OnEnable()
    {
        defalutText = text.text;
        isShort = true;
    }
    private void OnDisable()
    {
        text.text = defalutText;
        alert = "";
        
    }
    public void OkClicked()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
       
        if (resourceType == ResourceType.Diamond)
        {
            if (PlayerData.Instance.currentDiamond >= price)
            {
                PlayerData.Instance.currentDiamond -= price;
                gameObject.SetActive(false);
                ChestGruop.SetActive(true);
            }
            else 
            {
                if (isShort)
                {
                    isShort = false;
                    alert = "<color=#ff0000>" + " \n sorry, you don't own enough Diamond for this" + "</color>";
                    text.text += alert;
                }
            }
                

            }
        else
        {
            PlayerData.Instance.currentGold -= price;
            if (PlayerData.Instance.currentDiamond >= price)
            {
                PlayerData.Instance.currentDiamond -= price;
                gameObject.SetActive(false);
                ChestGruop.SetActive(true);
            }

            else
            {
                if (isShort)
                {
                    isShort = false;
                    alert = "<color=#ff0000>" + " \n sorry, you don't own enough Gold for this" + "</color>";
                    text.text += alert;
                }
            }
        }
        
        ResourceController.instance.UpdatePurchase();

    }
}
