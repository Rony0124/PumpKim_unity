using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class Purchase : MonoBehaviour
{
    /*public static Purchase instance;

    private void Awake()
    {
        instance = this;
    }*/
    public ResourceType rcType;
    public int priceTag;
    public bool isPurchased;
    public RectTransform useBtn;
    public int itemId;
    public GameObject text;
    public Purchase[] purchase;
    PlayerData playerData;
    [SerializeField] public AudioClip buttonCliclSFX;
    private void Start()
    {
        playerData = PlayerData.Instance;
    }
    private void Update()
    {
        if (isPurchased)
        {
            if(itemId < 10) 
            {
                useBtn.DOMove(GetComponent<RectTransform>().position, 0.08f);
            }
            else if (itemId >= 1000)
            {
                text.SetActive(true);
            }
            playerData.ownedItems.Add(itemId);
            playerData.ownedItems = playerData.ownedItems.Distinct().ToList();
            useBtn.gameObject.GetComponent<Button>().interactable = true;
            gameObject.SetActive(false);
        }
       
        
    }
    public void Click()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        Invoke("SetInfo", 0.3f);
    }
    void SetInfo()
    {
        ConfirmPopUp.instance.currentResourceType = rcType;
        ConfirmPopUp.instance.currentPrice = priceTag;
        ConfirmPopUp.instance.itemId = itemId;
        purchase = FindObjectsOfType<Purchase>();
        ConfirmPopUp.instance.purchase = purchase;
    }
   
    
}
