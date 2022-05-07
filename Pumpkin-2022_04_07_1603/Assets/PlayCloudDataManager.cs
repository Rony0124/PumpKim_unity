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
            Debug.Log("�α��� ����: " + Social.localUser.authenticated);
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
        Debug.Log("�α��� �õ�: " + onComplete);
        Social.localUser.Authenticate(onComplete);
    }



    #region Save
    //�ܺο��� �ҷ����� �Լ�
    public void SaveData()
    {
        if (isAuthenticated)
        {
            Debug.Log("�α��ε� ����ڰ� ���̺� �õ�");

            //loadedData = dataToSave;
            isSaving = true;
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution(m_saveFileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpen);

        }
        else
        {
            Debug.Log("�α��ε��� ���� ����ڰ� ���̺� �õ�");

            //�α��� ���н� ���ÿ� ����
            PlayerData.Instance.SavePlayer();
        }
    }

    private void SaveGame(ISavedGameMetadata data)
    {
        Debug.Log("Ŭ���忡 ������ ���̺�");

        //PlayerData.Instance.LoadPlayer();
        //SaveSystem.Save(new DataVO(PlayerData.Instance));
        //string a = PlayerData.Instance.cloundMng.GameInfoToString();
        //PlayerData.Instance.SetPlayInfo2Data();

        PlayerData.Instance.SavePlayer();

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
        var stringToSave = this.GameInfoToString();
        Debug.Log("���̺�" + stringToSave);
        byte[] bytes = StringToBytes(stringToSave);
        /*   sbyte[] SBytes = new sbyte[bytes.Length];
           Buffer.BlockCopy(bytes, 0, SBytes, 0, bytes.Length);*/

        //byte[] bytes = StringToBytes("�׽�Ʈ");//�׽�Ʈ ���̺� ����
        Debug.Log("�׽�Ʈ ���̺� ����: " + BytesToString(bytes));
        savedGameClient.CommitUpdate(data, update, bytes, OnSavedGameDataWrittenComplete);

    }




    #endregion



    #region Load
    public void LoadData()//Ŭ���� �ε� ����
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
        Debug.Log("Ŭ���忡�� ������ �ε�, �÷��̾� �����Ϳ� ������ ����");

        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(data, OnSavedGameDataReadComplete);
    }

    #endregion
    //save���Ӽ������ο� ���� ���
    private void OnSavedGameOpen(SavedGameRequestStatus status, ISavedGameMetadata metaData)
    {
        Debug.Log("Ŭ���� ���� �õ�");

        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Ŭ���� ���� �õ� ����");

            if (!isSaving)
            {
                Debug.Log("Ŭ���忡�� ������ �ε� �õ�");

                this.LoadGame(metaData);
            }
            else
            {
                Debug.Log("Ŭ���忡 ������ ���̺� �õ�");

                this.SaveGame(metaData);
            }
        }
        else
        {
            Debug.LogWarning("Ŭ���� ���� ���� : " + status);
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
