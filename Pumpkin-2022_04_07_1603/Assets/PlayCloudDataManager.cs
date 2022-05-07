// code reference: http://answers.unity3d.com/questions/894995/how-to-saveload-with-google-play-services.html		
// you need to import https://github.com/playgameservices/play-games-plugin-for-unity
using UnityEngine;
using System;
using System.Collections;
//gpg
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
//for encoding
using System.Text;
//for extra save ui
using UnityEngine.SocialPlatforms;
//for text, remove
using UnityEngine.UI;
public class PlayCloudDataManager : MonoBehaviour
{

    private static PlayCloudDataManager instance;

    public static PlayCloudDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayCloudDataManager>();

                if (instance == null)
                {
                    instance = new GameObject("PlayGameCloudData").AddComponent<PlayCloudDataManager>();
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
    public Action<SavedGameRequestStatus, byte[]> OnSavedGameDataReadComplete;
    public Action<SavedGameRequestStatus, ISavedGameMetadata> OnSavedGameDataWrittenComplete;
    public bool isSaving
    {
        get;
        private set;
    }
    public string loadedData
    {
        get;
        private set;
    }
    private const string m_saveFileName = "game_save_data1";

    public bool isAuthenticated
    {
        get
        {
            Debug.Log("로그인 상태: " + Social.localUser.authenticated);
            return Social.localUser.authenticated;
        }
    }

    public void InitiatePlayGames()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        .EnableSavedGames()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = false;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }

    public void LogIn(System.Action<bool> onComplete)
    {
        Debug.Log("로그인 시도: " + onComplete);
        Social.localUser.Authenticate(onComplete);
    }



    #region Save
    //외부에서 불러오는 함수
    public void SaveData()
    {
        if (isAuthenticated)
        {
            Debug.Log("로그인된 사용자가 세이브 시도");

            //loadedData = dataToSave;
            isSaving = true;
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution(m_saveFileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpen);

        }
        else
        {
            Debug.Log("로그인되지 않은 사용자가 세이브 시도");

            //로그인 실패시 로컬에 저장
            PlayerData.Instance.SavePlayer();
        }
    }

    private void SaveGame(ISavedGameMetadata data)
    {
        Debug.Log("클라우드에 데이터 세이브");

        //PlayerData.Instance.LoadPlayer();
        //SaveSystem.Save(new DataVO(PlayerData.Instance));
        //string a = PlayerData.Instance.cloundMng.GameInfoToString();
        //PlayerData.Instance.SetPlayInfo2Data();

        PlayerData.Instance.SavePlayer();

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
        var stringToSave = this.GameInfoToString();
        Debug.Log("세이브" + stringToSave);
        byte[] bytes = StringToBytes(stringToSave);
        /*   sbyte[] SBytes = new sbyte[bytes.Length];
           Buffer.BlockCopy(bytes, 0, SBytes, 0, bytes.Length);*/

        //byte[] bytes = StringToBytes("테스트");//테스트 세이브 내용
        Debug.Log("테스트 세이브 내용: " + BytesToString(bytes));
        savedGameClient.CommitUpdate(data, update, bytes, OnSavedGameDataWrittenComplete);

    }




    #endregion



    #region Load
    public void LoadData()//클라우드 로드 시작
    {
        Debug.Log("auth: " + isAuthenticated);
        if (isAuthenticated)
        {
            this.isSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(m_saveFileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, this.OnSavedGameOpen);

        }
        else
        {
            PlayerData.Instance.LoadPlayer(SaveSystem.Load());
            Debug.Log("loadplayer success");
        }
    }
    private void LoadGame(ISavedGameMetadata data)
    {
        Debug.Log("클라우드에서 데이터 로드, 플레이어 데이터에 데이터 전달");

        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(data, OnSavedGameDataReadComplete);
    }

    #endregion
    //save게임성공여부에 따른 경우
    private void OnSavedGameOpen(SavedGameRequestStatus status, ISavedGameMetadata metaData)
    {
        Debug.Log("클라우드 연결 시도");

        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("클라우드 연결 시도 성공");

            if (!isSaving)
            {
                Debug.Log("클라우드에서 데이터 로드 시도");

                this.LoadGame(metaData);
            }
            else
            {
                Debug.Log("클라우드에 데이터 세이브 시도");

                this.SaveGame(metaData);
            }
        }
        else
        {
            Debug.LogWarning("클라우드 연결 실패 : " + status);
            if (!isSaving)
            {
                PlayerData.Instance.LoadPlayer(SaveSystem.Load());//
            }
            else
            {
                PlayerData.Instance.SavePlayer();
            }

        }
    }
    private byte[] StringToBytes(string stringToConvert)
    {
        return Encoding.UTF8.GetBytes(stringToConvert);
    }

    private string BytesToString(byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes);
    }

    public void StringToGameInfo(string localData)
    {
        if (localData != string.Empty)
        {
            SaveSystem.Save(JsonUtility.FromJson<DataVO>(localData));
        }
    }
    public string GameInfoToString()
    {
        string jsonString = JsonUtility.ToJson(new DataVO(PlayerData.Instance));
        Debug.Log("json: " + jsonString);
        return jsonString;
    }
}
