using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    static UIController instance;
    public static UIController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIController>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("UIController");
                    instance = instanceContainer.AddComponent<UIController>();

                }
            }
            return instance;
        }
    }
    [Header("PlayerButton")]
    public GameObject JoyStickGO;
    public GameObject AtkButtonGO;

    [Header("EndOfGame")]
    public GameObject EndGameGO;
    public GameObject GameClearGO;
    //public TextMeshProUGUI goldText;
    //public TextMeshProUGUI scoreText;

    [Header("SkillSelect")]
    public GameObject SkillSelectGO;
    //public TextMeshProUGUI SkillSelectTxt;
    [Header("GainedItems")]
    public GameObject ItemSlot;
    public Text txt;
    [Header("Audio")]
    [SerializeField] public AudioClip audioClip;
    [SerializeField] public AudioClip audioClipBoss;
    [SerializeField] public AudioClip buttonCliclSFX;
    [SerializeField] public AudioClip buttonCliclSFX2;
    [Header("Etc")]
    public GameObject HpSlider;
    public bool bossRoom = false;
    public GameObject DmgBGEffect;
    public GameObject InventoryButtonGO;
    public GameObject BossAlert;

    private void Start()
    {
        StartCoroutine(PlayMusicField());
    }

    IEnumerator PlayMusicField()
    {
        yield return new WaitForSeconds(2f);
        // Time.timeScale = 1;
        AudioMng.Instance.PlayMusicWithFade(audioClip);
    }


    public void EndGame()
    {
        //Debug.Log("이부분에러");
        //PlayerData.Instance.SetScore();
        JoyStickGO.gameObject.SetActive(false);
        AtkButtonGO.gameObject.SetActive(false);
        HpSlider.SetActive(false);
        //InventoryButtonGO.gameObject.SetActive(false);
        StartCoroutine(EndGamePopUp());
    }

    IEnumerator EndGamePopUp()
    {
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1.5f);
        EndGameGO.SetActive(true);
        //goldText.text = txt.text;
        //scoreText.text = StageMng.Instance.currentStage.ToString();
        yield return new WaitForSecondsRealtime(2f);
    }
    public void GameClear()
    {
        //PlayerData.Instance.SetScore();
        HpSlider.SetActive(false);
        JoyStickGO.gameObject.SetActive(false);
        AtkButtonGO.gameObject.SetActive(false);
        //InventoryButtonGO.gameObject.SetActive(false);
        //SkillSelectGO.gameObject.SetActive(false);
        StartCoroutine(GameClearPopUp());
    }

    IEnumerator GameClearPopUp()
    {
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1.5f);
        GameClearGO.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
    }

    //스킬선택창 활성화
    public void SkillSelect()
    {
        SkillSelectGO.gameObject.SetActive(true);
        //SkillSelectTxt.text += StageMng.Instance.currentStage.ToString();
    }
    //로비로갈때 로비로 가기전과 같은상태로 돌리자
    public void ToLobby()
    {
        Time.timeScale = 1;
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        //Lobby로 갔을때 데이터 default로 초기화
        PlayerData.Instance.LobbyData(SaveSystem.Load());
        PlayerData.Instance.currentHp = PlayerData.Instance.maxHp;
        //playscene에서의 stage데이터 초기화
        StageMng.Instance.RemoveStageMng();
        SceneManager.LoadScene("1StartPage");
        PlayCloudDataManager.Instance.SaveData();
        PlayerData.Instance.SetScore();

    }

    public void Continue()
    {
        Time.timeScale = 1;
        JoyStickGO.gameObject.SetActive(true);
        AtkButtonGO.gameObject.SetActive(true);
        GameClearGO.SetActive(false);
    }

    //아이템 픽없했을경우 화면좌측상단에 아이템아이콘생성
    public void ItemPIckUp(int itemId)
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX2);
        ItemSlot.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        Sprite itemIcon = Resources.Load("Item_Icons/" + itemId.ToString(), typeof(Sprite)) as Sprite;
        ItemSlot.GetComponent<Image>().sprite = itemIcon;
    }

    public void ItemSlotClear()
    {
        ItemSlot.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        ItemSlot.GetComponent<Image>().sprite = null;
    }

    //골드 획득시 화면 좌측상단에 표시
    public void GainGold(int gainedGold)
    {
        int currentGold = int.Parse(txt.text);
        currentGold += gainedGold;
        txt.text = currentGold.ToString();
    }
    //데미지 이팩트
    public void Damage()
    {
        DmgBGEffect.transform.GetComponent<Animator>().SetTrigger("Hit");
    }

    public void BossRoomEnter()
    {

        BossAlert.SetActive(true);
        // Time.timeScale = 0;
        StartCoroutine(DisableAlert());
        StartCoroutine(PlayMusicBossRoom());

    }

    IEnumerator PlayMusicBossRoom()
    {
        yield return new WaitForSeconds(2f);
        // Time.timeScale = 1;
        AudioMng.Instance.PlayMusicWithFade(audioClipBoss);
    }

    IEnumerator DisableAlert()
    {
        yield return new WaitForSeconds(2f);
        // Time.timeScale = 1;
        BossAlert.SetActive(false);
    }
}