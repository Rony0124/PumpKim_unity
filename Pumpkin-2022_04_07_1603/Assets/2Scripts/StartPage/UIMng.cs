using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIMng : MonoBehaviour
{
    public static UIMng instance;

    void Awake()
    {

        if (instance == null) instance = this;
        else if (instance != null) return;

        //---------------새로추가한 부분---------------
        //GooglePlayServiceManager.Instance.Login(); //로그인 시도
        //---------------새로추가한 부분---------------
    }

    [Header("Configuration")]
    public GameObject SoundConfig;
    //public GameObject GameConfig;

    [Header("Skill&Weapon")]
    public GameObject SkillPanel;
    public GameObject SkillHeader;
    public GameObject WeaponPanel;
    public GameObject WeaponHeader;
    [Header("Audio")]
    [SerializeField] public AudioClip audioClip;
    [SerializeField] public AudioClip audioClip2;
    [SerializeField] public AudioClip buttonCliclSFX;
    [SerializeField] public AudioClip buttonCliclSFX2;
    [SerializeField] public AudioClip buttonCliclSFX3;

    public GameObject RandomDraw;
    public GameObject RandomDrawEffect;
    public GameObject Deck;
    public Purchase[] purchase;
    PlayerData playerData;
   // public Text texti;
   // public Text texti2;

    //public RawImage myImage;
    public Text myId;
    public bool success;
    private void Start()
    {
        StartCoroutine(SetInfo());
        
        AudioMng.Instance.PlayMusic(audioClip);
        playerData = PlayerData.Instance;



        //---------------새로추가한 부분---------------
        Invoke("ComplateFirstBloodWrapper", 3.0f);

    }

    //---------------새로추가한 부분---------------
    private void ComplateFirstBloodWrapper()
    {
        if (GooglePlayServiceManager.Instance.isAuthenticated) //만약 로그인이 되어있으면 실행
        {
            GooglePlayServiceManager.Instance.CompleteFirstBlood(); //게임을 시작하자 마자 바로 완수하는 과제
        }

    }
    //---------------새로추가한 부분---------------




    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < purchase.Length; i++)
        {
            for (int j = 0; j < playerData.ownedItems.Count; j++)
            {
                if (purchase[i].itemId == playerData.ownedItems[j])
                {
                    purchase[i].isPurchased = true;
                }
            }

        }
    }
    IEnumerator SetInfo()
    {
        yield return new WaitForSeconds(0.5f);
        //GooglePlayMng.Instance.onBtnLoginClicked();
        myId.text = PlayerData.Instance.playerId;
    }
    //save
    public void OnSaveClick()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        playerData.SavePlayer();
    }
    //game exit
    public void OnExitClick()
    {
        PlayCloudDataManager.Instance.SaveData();
        Application.Quit();
    }
    public void OnCharClick()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        purchase = FindObjectsOfType<Purchase>();
    }
    //sound header click
    public void OnSoundClick()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        SoundConfig.SetActive(true);
       // GameConfig.SetActive(false);
    }
    //game header click
    public void OnGameClick()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        SoundConfig.SetActive(false);
        //GameConfig.SetActive(true);
    }
    public void OnSkillClick()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        SkillPanel.GetComponent<RectTransform>().DOLocalMove(Vector2.zero, 0.5f);
        WeaponPanel.GetComponent<RectTransform>().DOLocalMove(new Vector2(0, -1000), 0.5f);

    }
    public void OnWeaponClick()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        SkillPanel.GetComponent<RectTransform>().DOLocalMove(new Vector2(0, -1000), 0.5f);
        WeaponPanel.GetComponent<RectTransform>().DOLocalMove(Vector2.zero, 0.5f);

    }
   
    //char header click
    public void OnCharClick1()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        SkillHeader.GetComponent<RectTransform>().DOLocalMove(new Vector2(-299, 269), 0.5f);
        WeaponHeader.GetComponent<RectTransform>().DOLocalMove(new Vector2(12, -269), 0.5f);
    }
    public void OnExitClick1()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        SkillHeader.GetComponent<RectTransform>().DOLocalMove(new Vector2(-299, -760), 0.5f);
        WeaponHeader.GetComponent<RectTransform>().DOLocalMove(new Vector2(12, -760), 0.5f);
    }

    public void OnDrawClick()
    {
        //button click
        AudioMng.Instance.PlaySFX(buttonCliclSFX3);
        StartCoroutine(DrawPopEnable());
    }

    IEnumerator DrawPopEnable()
    {
        yield return new WaitForSeconds(3.5f);
        
        RandomDraw.SetActive(false);
        RandomDrawEffect.SetActive(false);

        yield return new WaitForSeconds(0.3f);
        //bgm
        AudioMng.Instance.PlayMusicWithCrossFade(audioClip2);
        Deck.SetActive(true);
    }
}
