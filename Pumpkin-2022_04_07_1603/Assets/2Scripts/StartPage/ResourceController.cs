
using UnityEngine;
using UnityEngine.UI;
public enum ResourceType
{
    Gold, Diamond
}
public class ResourceController : MonoBehaviour
{
    public static ResourceController instance;

    void Awake()
    {

        if (instance == null) instance = this;
        else if (instance != null) return;

    }
    public Text text;
    public ResourceType rcType;

    public int number;
    public bool updateUI;
    bool pointerDown;
    // Start is called before the first frame update
    void Start()
    {
        if (rcType.ToString() == "Gold") number = PlayerData.Instance.currentGold;
        else number = PlayerData.Instance.currentDiamond;

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (updateUI)
        {
            updateUI = false;
            UpdateUI();
        }
        if (pointerDown)
        {
            pointerDown = false;
            IncreaseNumber();
           
        }
    }
    public void UpdateUI()
    {
        Debug.Log("UIUpdated");
       
        if (rcType.ToString() == "Gold") PlayerData.Instance.currentGold = number;
        else PlayerData.Instance.currentDiamond= number;
        text.text = number.ToString();
        //playdata안의 data에다 저장데이터 집어넣기
        //PlayerData.Instance.();
       // PlayCloudDataManager.Instance.SaveData();
    }
    public void UpdatePurchase()
    {

        if (rcType.ToString() == "Gold") number = PlayerData.Instance.currentGold;
        else number = PlayerData.Instance.currentDiamond;
        text.text = number.ToString();

        PlayCloudDataManager.Instance.SaveData();
    }

    void IncreaseNumber()
    {
        number += 50;
        updateUI = true;
    }

   

    public void PointerDown()
    {
        pointerDown = true;
    }
    public void PointerUP()
    {
        pointerDown = false;
    }
}
