using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayServiceManager : MonoBehaviour
{

    static private GooglePlayServiceManager instance; // 언제 어디서든 접근 가능하게 싱글턴으로 만듬
    


    public static GooglePlayServiceManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GooglePlayServiceManager>(); // 인스턴스가 없으면 찾아보고

                if (instance == null)
                {
                    //그래도 없으면 새로운 게임오브젝트를 만들고 컴포넌트를 붙여준다.
                    instance = new GameObject("Google Play Service").AddComponent<GooglePlayServiceManager>(); 
                }
            }

            return instance;
        }
    }

    // Start is called before the first frame update



    public void Login() //수동 로그인
    {
        //localUser 자기 자신 인증을 시도하는데 입력으로 인증을 성공하거나 실패했을때 그 결과를 알 수 있는 함수를 넣어달라고함
        Social.localUser.Authenticate((bool success) => { if (!success) { Debug.Log("로그인 실패"); } });
        //함수를 발동시켜 로그인 실패 여부를 로그로 남김
    }


    public bool isAuthenticated { //사용자가 로그인이 됐는지 않됐는지 체크
        get {
            return Social.localUser.authenticated;
        }
    }


    
    public void CompleteFirstBlood()//도전과제 완수 함수
    {
        if (!isAuthenticated) //만약 로그인이 안되어있다면
        {
            Login(); //로그인을 시도하고 리턴
            return;
        }


        //첫번째는 아이디, 두번째는 완수율, 세번째는 callback = ReportProgress가 끝났을 때 그 결과에 대해서 전달 받을 수 있는 함수
        Social.ReportProgress(GPGSIds.achievement_hello_world, 100.0, (bool success) =>
        {
            if (!success) { Debug.Log("도전과제가 실패!"); }
        });
    }

    /* public void CompleteSowMeTheMoney()//가상 두번째 도전과제 완수 함수 예)골드 1000개 모으기?
     {
         
         if (!isAuthenticated) //만약 로그인이 안되어있다면
         {
             Login(); //로그인을 시도하고 리턴
             return;
         }

         //첫번째는 아이디, 두번째는 완수율, 세번째는 callback = ReportProgress가 끝났을 때 그 결과에 대해서 전달 받을 수 있는 함수
         Social.ReportProgress(GPGSIds.leaderboard_1, 100.0, (bool success) =>
         {
             //ReportProgress가 성공했을 경우 PlayerPrefs에 저장해서 도전과제가 다시 수행되지 않게 한다.
             if (success) {PlayerPrefs.SetInt("ShowMeTheMoney", 1); }

         });
     }*/

    

    public void ShowAchivementUI() //업적보기 이미지를 불러오는 함수
    {
        if (!isAuthenticated) //만약 로그인이 안되어있다면
        {
            Login(); //로그인을 시도하고 리턴
            return;
        }


        Social.ShowAchievementsUI();
    }


    // 리더보드(순위)UI 불러오기--------------------------------------------------------
    public void ShowLeaderboardUI_1() => ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_episode1__achieve_the_highest_score);
    public void ShowLeaderboardUI_2() => ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_episode2_achieve_the_highest_score);
    public void ShowLeaderboardUI_3() => ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_episode3_achieve_the_highest_score);
    // --------------업적 UI 불러오기 끝------------------------------------------------


   
    // 리더보드(순위)에 값 대입 시켜주기 ------------------------------------------------------------------
    public void AddLeaderboard_1() => Social.ReportScore(PlayerData.Instance.records[0].bestStage, GPGSIds.leaderboard_episode1__achieve_the_highest_score, (bool success) => { SucessDebug(); });
    public void AddLeaderboard_2() => Social.ReportScore(PlayerData.Instance.records[1].bestStage, GPGSIds.leaderboard_episode2_achieve_the_highest_score, (bool success) => { SucessDebug(); });
    public void AddLeaderboard_3() => Social.ReportScore(PlayerData.Instance.records[2].bestStage, GPGSIds.leaderboard_episode3_achieve_the_highest_score, (bool success) => { SucessDebug(); });
    // ------------------ 리더보드(순위)에 값 대입 시켜주기 끝 ---------------------------------------------

    public void SucessDebug()
    {
        Debug.Log("업적 넣기 성공");
    }
}
