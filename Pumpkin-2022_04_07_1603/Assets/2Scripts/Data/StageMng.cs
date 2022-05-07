using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMng : MonoBehaviour
{
    static StageMng instance;
    public static StageMng Instance
    {
        get
        {
            return instance;
        }
    }
    public int currentStage = 1;
    public int currentEp = 1;
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
        currentEp = PlayerData.Instance.currentEpisode;
    }
   
    public void NextStage()
    {
       
        if (currentStage == 3)
        {
            
            //게임클리어시 다음에피소드 활성화
            PlayerData.Instance.records.Add(new RecordVO(0, false));
            Debug.Log("asdsad");

            UIController.Instance.GameClear();
        }
        else
        {
            Debug.Log("Get next room");

            StartCoroutine(Reload());
            //return;
        }
        PlayerData.Instance.SetScore();
        currentStage++;
    }
    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void RemoveStageMng()
    {
        Destroy(this.gameObject);
    }
}
