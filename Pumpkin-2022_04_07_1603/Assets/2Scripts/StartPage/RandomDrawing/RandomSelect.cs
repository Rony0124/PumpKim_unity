using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class RandomSelect : MonoBehaviour
{
    public static RandomSelect instance;

    void Awake()
    {

        if (instance == null) instance = this;
        else if (instance != null) return;

    }


    public List<Card> deck = new List<Card>();  // 카드 덱
    public float total = 0;  // 카드들의 가중치 총 합
    public int cardCount;
    public List<Card> result = new List<Card>();  // 랜덤하게 선택된 카드를 담을 리스트
    public List<CardUI> cards = new List<CardUI>();
    public Card chosenCard;
    
    [Header("DrawPanel")]
    public Transform parent;
    public GameObject cardprefab;
    public TextMeshProUGUI cardGrade;
    public GameObject gradeEffect;
    public GameObject OkBtn;
    public Text rewardAlert;
    [SerializeField] public AudioClip audioClip;
    [SerializeField] public AudioClip buttonCliclSFX;
    [SerializeField] public AudioClip buttonCliclSFX2;
    void OnEnable()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            // 스크립트가 활성화 되면 카드 덱의 모든 카드의 총 가중치를 구해줍니다.
            total += deck[i].weight;
        }
        parent.gameObject.SetActive(true);
        cardGrade.color = new Color(0f,0f,0f,0f);
        // 실행
        ResultSelect();

    }

    

    public void ResultSelect()
    {
        for (int i = 0; i < cardCount; i++)
        {
            // 가중치 랜덤을 돌리면서 결과 리스트에 넣어줍니다.
            result.Add(RandomCard()); 
           
            // 비어 있는 카드를 생성하고
            CardUI cardUI = Instantiate(cardprefab, parent).GetComponent<CardUI>();
            cardUI.transform.localPosition = new Vector3(cardUI.transform.localPosition.x + ((i -1)*320), cardUI.transform.localPosition.y);
            // 생성 된 카드에 결과 리스트의 정보를 넣어줍니다.
            cards.Add(cardUI);
            cardUI.CardUISet(result[i], i);
            
        }
    }
    // 가중치 랜덤의 설명은 영상을 참고.
    public Card RandomCard()
    {
        float weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));
        for (int i = 0; i < deck.Count; i++)
        {
            weight += deck[i].weight;
            if (selectNum <= weight)
            {
                Card temp = new Card(deck[i]);
                return temp;
            }
        }
        return null;
    }
    bool isExist;
    public void FilpAll()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX2);
        for (int i =0; i < cards.Count; i++)
        {
            cards[i].GetComponent<Animator>().SetTrigger("Flip");
            OkBtn.GetComponent<Button>().interactable = true;
            Debug.Log(i + "asd" + cards[i].isChosen);
            
            if (cards[i].isChosen)
            {
                cards[i].transform.localScale = Vector2.Lerp(cards[i].transform.localScale, new Vector2(3f, 3f), 0.1f);
                gradeEffect.SetActive(true);
                cardGrade.text = cards[i].card.cardGrade.ToString();
                cardGrade.color = new Color(1f,0.95f,0f,1f);
                chosenCard = cards[i].card;
                Debug.Log(isExist);
                isExist = PlayerData.Instance.ownedItems.Exists(x => x == chosenCard.itemId);
                Debug.Log(isExist);

                if (!isExist)
                {
                    PlayerData.Instance.ownedItems.Add(chosenCard.itemId);
                    PlayerData.Instance.ownedItems = PlayerData.Instance.ownedItems.Distinct().ToList();
                }else
                {
                    int reward = (int)(500f / chosenCard.weight);
                    PlayerData.Instance.currentDiamond += reward;
                    rewardAlert.text = "Already in your possession, so we grant <color=#6BE0FA>+" + reward.ToString() + " diamond</color>";
                    ResourceController.instance.UpdatePurchase();
                }
                
                PlayCloudDataManager.Instance.SaveData();//클라우드에 데이터를 세이브 하는데 데이터는 최신화가 되어 있지 않다.
                //StartCoroutine(CloseTab());
                //return;
            }
        }
        AudioMng.Instance.StopMusic();
    }
    public void CloseTab()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        //yield return new WaitForSeconds(3f);
        //OkBtn.SetActive(true);
        for (int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i].gameObject);
        }
        total = 0;
        result.Clear();
        cards.Clear();
        cardGrade.text = "";
        gradeEffect.SetActive(false);
        OkBtn.GetComponent<Button>().interactable = false;
        AudioMng.Instance.PlayMusicWithCrossFade(audioClip);
        parent.gameObject.SetActive(false);
        
        
        gameObject.SetActive(false);


    }
 

}
