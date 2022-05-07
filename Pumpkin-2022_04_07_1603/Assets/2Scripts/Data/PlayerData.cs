using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.UI;
using UnityEditor;

public class PlayerData : MonoBehaviour
{
    static PlayerData instance;
    public static PlayerData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerData>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerData");
                    instance = instanceContainer.AddComponent<PlayerData>();

                }
            }
            return instance;
        }
    }
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    //데이터 저장목록
    //player 현재 stat정보
    [Header("PlayerStats")]
    public int maxHp;
    public int currentHp;
    public float dmg;
    public float critical = 0.15f;
    public float atkSpd = 1;
    [Header("Resource")]
    public int currentGold;// 현재 gold
    public int currentDiamond;
    public List<int> ownedItems = new List<int>();//player가 puecahse한 아이템
    //record
    [Header("Records")]
    [SerializeField]
    public List<RecordVO> records = new List<RecordVO>();

    [Header("ItemInfoInPlay")]
    public GameObject[] PlayerBolt;  //발사체종류
    public string prevWeapon;
    public string weaponName;
    public List<int> PlayerSkill = new List<int>(); //player가 장착한 skill 리스트(int로 관리)
    public int currentEpisode;
    //플레이중 획득 아이템 관리 ex)key, quest items
    public List<int> ItemList = new List<int>();

    public string playerId;
    public PlayCloudDataManager cloundMng;
    DatabaseMng database;

    //bool playerDead = false;
    bool isSkill = true;
    bool setedScore = false;
    //직접 데이터를 건들지 않기위한 변수
    public int playDamage;
    public float playCritical = 0.01f;
    public float playAtkSpd = 1;
    public float weaponDamageToPlus;
    public float damageMagnification = 1;
    public float weaponCriticalToPlus;
    public float weaponAtkSpdToPlus;
    // public Text tt;
    public GameObject loadimg;
    IEnumerator Start()
    {
        database = FindObjectOfType<DatabaseMng>();
        // cloundMng = PlayCloudDataManager.Instance;
        yield return new WaitForEndOfFrame();
        loadimg.SetActive(true);
        this.cloundMng.OnSavedGameDataReadComplete = (status, bytes) => {
            if (status == SavedGameRequestStatus.Success)
            {
                Debug.Log("클라우드에서 데이터 로드 결과: " + status);

                string strCloudData = null;

                if (bytes.Length == 0)
                {
                    strCloudData = string.Empty;
                    Debug.Log("로드 성공, 데이터 없음, 로컬 세이브 로드");
                    //tt.text = "로드 성공, 데이터 없음";
                    //this.maxHp = 100;
                    LoadPlayer(SaveSystem.Load());
                    
                }
                else
                {
                    Debug.Log("로드 성공, 로드 데이터: " + Encoding.UTF8.GetString(bytes));//테스트 로드 내용 테스트가 나와야 한다.
                    strCloudData = Encoding.UTF8.GetString(bytes);
                    Debug.Log("로드 데이터 로컬 세이브에 저장");
                    SaveSystem.Save(JsonUtility.FromJson<DataVO>(strCloudData));
                    Debug.Log("로컬 세이브 게임에 로드");
                    LoadPlayer(SaveSystem.Load());
                    //if()


                    //tt.text = data.Diamond.ToString();
                }
            }
            else
            {
                Debug.Log(string.Format("로드 실패 : {0}", status));
                //tt.text = string.Format("로드 실패 : {0}", status);
            }
            //add loading
        };

        this.cloundMng.OnSavedGameDataWrittenComplete = (status, game) => {
            Debug.Log("클라우드에 데이터 세이브 결과: " + status);

            if (status == SavedGameRequestStatus.Success)
            {
                Debug.Log("클라우드에 저장 하였습니다.");
                // tt.text = "클라우드에 저장 하였습니다.";
            }
        };

        this.cloundMng.InitiatePlayGames();

        this.cloundMng.LogIn((result) =>
        {
            //result = true;

            if (result)
            {
                // this.txtUserName.text = Social.localUser.userName;

                playerId = Social.localUser.userName;

                StartCoroutine(this.Init());
                // this.Init();
            }
            else
            {
                Debug.Log("로그인 실패");
                //this.txtUserName.text = "로그인 실패";

                StartCoroutine(this.Init());
                //this.Init();
            }

        });
        playCritical = critical;
        playAtkSpd = atkSpd;
        //data = SaveSystem.Load();W

        
    }
    void Update()
    {
        //데미지 받았을경우 이벤트에 추가하기
        //hp가 0이하일경우
        if (currentHp <= 0)
        {
            if(setedScore == false)
            {
                Debug.Log("Setscore 실행");
                SetScore();
                setedScore = true;
            }
            //PlayCloudDataManager.Instance.SaveData();
            UIController.Instance.EndGame();
            return;
        }
        //플레이신에서 로비로 돌아왔을 경우

        //---------------새로추가한 부분---------------
        /*if (currentGold > 1000 && PlayerPrefs.HasKey("ShowMeTheMoney")) //도전과제를 완수하지않고 골드가 1000보다 크면
        {
            GooglePlayServiceManager.Instance.CompleteSowMeTheMoney(); // 가상 두번째 임무완수
        }*/
        //---------------새로추가한 부분---------------

    }
    IEnumerator Init()
    {

        var path = Application.persistentDataPath + "playerData9.bin";
        Debug.Log(path);
        Debug.LogFormat("로컬 세이브 Exists: {0}", File.Exists(path));

        //로컬 세이브가 있던 없던 문제가 안댐, 로컬세이브가 없다면 클라우드에서 데이터를 가져와 저장시키면 로컬 세이브가 생김
        this.cloundMng.LoadData();//클라우드에서 데이터 로드
        SaveSystem.Save(new DataVO(this));//로드한 데이터 세이브 -> 로컬 세이브 생성, 있었어도 클라우드 데이터로 덮어씌어짐



        /*if (File.Exists(path))
        {
            this.cloundMng.LoadData();
        }
        else
        {
            string a = cloundMng.GameInfoToString();//제이슨은 빈값
            
            SaveSystem.Save(new DataVO(this));
            Debug.Log("save complete");//여기가 문제
            a = cloundMng.GameInfoToString();//제이슨은 빈값
            //Invoke("LoadPlayer", 1f);
            //this.cloundMng.SaveData(); 이미 클라우드에 데이터가 있으면 문제가 생김
            this.cloundMng.LoadData();// 클라우드에 데이터가 있을 수 있음으로 로드
            Debug.Log("load complete");
            *//*  this.data = new DataVO();

              //this.gameInfo.userId = Social.localUser.id;
              var json = JsonUtility.ToJson(this.data);
              byte[] bytes = Encoding.UTF8.GetBytes(json);
              File.WriteAllBytes(path, bytes);*//*
        }*/

        yield return new WaitForSeconds(2f);
        loadimg.SetActive(false);
        yield return new WaitForEndOfFrame();
        UIManager.instance.BtnMove();

    }

    public void ApplyWeapon()
    {
        //현재선택한 아이템의 정보 db에서 가져와 apply

        prevWeapon = weaponName;
        weaponName = DataMng.instance.currentWeapon.ToString();
        for (int i = 0; i < database.weaponList.Count; i++)
        {

            if (weaponName == database.weaponList[i]._name)
            {
                weaponDamageToPlus = database.weaponList[i]._dmg;
                weaponCriticalToPlus = database.weaponList[i]._critical;
                weaponAtkSpdToPlus = database.weaponList[i]._atkSpd;
                return;
            }

        }
    }
    //기록저장
    public void SetScore()
    {
        int ep = currentEpisode - 1;
        //기존플레이 데이터가 없으면 default로 설정
        if (records[ep] == null)
        {
            records[ep] = new RecordVO(StageMng.Instance.currentStage, false);
        }
        //데이터가 있을경우 기록비교후 저장
        else
        {
            if (records[ep].bestStage < StageMng.Instance.currentStage)//현재 스테이지가 베스트 보다 크다면
            {

                if (StageMng.Instance.currentStage >= 3)//3스테이지보다 현재 스테이지가 크다면
                {
                    Debug.Log(StageMng.Instance.currentStage);
                    records[ep] = new RecordVO(StageMng.Instance.currentStage, true);
                }
                else
                {
                    records[ep] = new RecordVO(StageMng.Instance.currentStage, false);
                }

            }
        }

        //리더보드 순위에 값을 넣어준다.-------- 새로 추가한 부분
        if (cloundMng.isAuthenticated)
            addLeaderBoard();
        //리더보드 순위에 값을 넣어준다.--------

        PlayCloudDataManager.Instance.SaveData();

    }

    private void addLeaderBoard()
    {
        switch (records.Count)
        {
            case 3:
                GooglePlayServiceManager.Instance.AddLeaderboard_3();
                goto case 2;
            case 2:
                GooglePlayServiceManager.Instance.AddLeaderboard_2();
                goto case 1;
            case 1:
                GooglePlayServiceManager.Instance.AddLeaderboard_1();
                return;
            default:
                break;
        }
    }

    //스킬선택했을때
    public void CheckSkill(int index)
    {
        Debug.Log(index);
        Debug.Log("heyhey");

        if (PlayerSkill[index] > 1)
        {
            if (index == 1 || index == 3)
            {
                //playDamage = Convert.ToInt32(playDamage * 0.7);
                playDamage += 10;
                Debug.Log(playDamage);
            }
            else if (index == 2 || index == 4)
            {
                playCritical += 0.03f;
            }
            else if (index == 5)
            {
                //데이터베이스에서 데미지 상수 가지고 오기 later
                playDamage += 20;
            }
            else if (index == 6)
            {
                playAtkSpd += 0.25f;
                //공속 + 25%
            }
            else if (index == 7)
            {
                maxHp += 1;
                currentHp += 1;
                //체력
            }
            else if (index == 8)
            {
                playCritical += 0.05f;
                //크리 + 5%
            }
            else
            {
                playDamage += 35;
                //공격력 대폭상승
            }
            return;
        }
        else
        {

            if (index <= 4)
            {
                playDamage = Convert.ToInt32(playDamage * 0.7);
                Debug.Log(playDamage);
            }
            else if (index == 5)
            {
                //데이터베이스에서 데미지 상수 가지고 오기 later
                playDamage += 20;
            }
            else if (index == 6)
            {
                playAtkSpd += 0.25f;
                //공속 + 25%
            }
            else if (index == 7)
            {
                maxHp += 1;
                currentHp += 1;
                //체력
            }
            else if (index == 8)
            {
                playCritical += 0.05f;
                //크리 + 5%
            }
            else
            {
                playDamage += 35;
                //공격력 대폭상승
            }
            return;
        }


    }
    public void SkillSelected()
    {
        //현재선택한 아이템의 정보 db에서 가져와 apply
        int skillIndex = (int)DataMng.instance.currentSkill;

        PlayerSkill[skillIndex] = 1;

        for (int i = 1; i < PlayerSkill.Count; i++)
        {
            if (PlayerSkill[i] != 0 && !isSkill)
            {

                //dmg = CalcDmg(0.7f, 0);
                damageMagnification = 0.7f;
                isSkill = true;
                return;
            }
        }
        if (PlayerSkill[0] != 0)
        {
            //dmg = CalcDmg(10/7, 0);
            isSkill = false;
            damageMagnification = 1;
        }
    }
    public void SkillDeselected(int index)
    {
        PlayerSkill[index] = 0;
    }

    public void CalcDmg()
    {
        playDamage = Convert.ToInt32((dmg + weaponDamageToPlus) * damageMagnification);
        playAtkSpd = atkSpd + weaponAtkSpdToPlus;
        playCritical = critical + weaponCriticalToPlus;
    }
    /*public DataVO SetPlayInfo2Data()
    {
        DataVO data = new DataVO();

        data.Diamond = currentDiamond;
        data.Gold = currentGold;
        data.ownedItems = ownedItems;
        data.dmg = dmg;
        data.hp = maxHp;
        data.critical = critical;
        data.atkSpd = atkSpd;
        data.records = records;
        data.ownedItems = ownedItems;

        return data;
    }*/
    public void SavePlayer()
    {
        SaveSystem.Save(new DataVO(this));
        LoadPlayer(SaveSystem.Load());
        string a = PlayerData.Instance.cloundMng.GameInfoToString();//로그 호출
        //cloundMng.SaveData();
        //GoogleCloudSaving.Save(this);
    }

    public void LoadPlayer(DataVO data)// 이거가 문제
    {
        if (SaveSystem.Load() == null)
        {
            SaveSystem.Save(new DataVO(this));
        }
        else
        {
            //data = SaveSystem.Load();
            currentDiamond = data.Diamond;
            currentGold = data.Gold;
            ownedItems = data.ownedItems;
            dmg = data.dmg;
            maxHp = data.hp;
            critical = data.critical;
            atkSpd = data.atkSpd;
            records = data.records;
            ownedItems = data.ownedItems;
        }
        setedScore = false;
    }
    public bool LobbyData(DataVO data)
    {
        if (data == null)
        {
            return false;
        }
        else
        {
            //새로추가 세이브 시스템 다시 로드
            data = SaveSystem.Load();

            dmg = data.dmg;
            maxHp = data.hp;
            critical = data.critical;
            atkSpd = data.atkSpd;
            for (int i = 0; i < PlayerSkill.Count; i++)
            {
                PlayerSkill[i] = 0;
            }
            SkillSelected();
            //PlayerSkill.Clear();
            return true;
        }
    }


}
