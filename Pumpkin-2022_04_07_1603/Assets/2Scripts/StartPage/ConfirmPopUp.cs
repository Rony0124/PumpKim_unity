using UnityEngine;
using UnityEngine.UI;

public class ConfirmPopUp : MonoBehaviour
{
    public static ConfirmPopUp instance;
    [SerializeField] public AudioClip buttonCliclSFX;
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;

    }
    public ResourceController[] resources;
    public ResourceType currentResourceType;
    public int currentPrice;
    public int itemId;
    public Text text;
    string startText;
    public Purchase[] purchase;
    // Start is called before the first frame update
    void Start()
    {
        resources = FindObjectsOfType<ResourceController>();
        startText = text.text;
        //purchase = UIpurchase;
    }

    int itemIndex;
    public void ConfirmClick()
    {
        
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        for (int i = 0; i< purchase.Length; i++)
        {
            if (purchase[i].itemId == itemId)
            {
                itemIndex = i;
            }
            
        }
        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].rcType == currentResourceType && resources[i].number > currentPrice)
            {
                resources[i].number -= currentPrice;
                //PlayerData.Instance.curre
                resources[i].updateUI = true;
                //Debug.Log(itemIndex);
                purchase[itemIndex].isPurchased = true;
                gameObject.SetActive(false);
                //PlayerData.Instance.SavePlayer();
                Invoke("AutoSave4Purchase", 1.5f);
                Debug.Log("saved");
                //return;
            } else if (resources[i].rcType == currentResourceType && resources[i].number < currentPrice)
            {
                text.text += "<color=#ff0000>" +"\n잔액이 부족합니다"+"</color>";
                return;
            }
        }
        
    }
    public void AutoSave4Purchase()
    {
        PlayCloudDataManager.Instance.SaveData();
        //PlayCloudDataManager.Instance.SaveData();
    }
    public void Delete()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        text.text = startText;
    }
}
