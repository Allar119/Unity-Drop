using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PlayServices : MonoBehaviour {

    public static PlayServices instance;

    void Awake()
    {
        //Singelton pattern (restricts the instantiation of a class to one object):
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //Dont destroy gameobject when new scene is loaded:
        DontDestroyOnLoad(gameObject);
    }



    // Use this for initialization
    void Start ()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        SignIn();
    }
	
    private void SignIn()
    {
        // authenticate user:
        Social.localUser.Authenticate(success => { });
    }

    #region Leaderboards

    public static void AddScoreToLeaderboard(string leaderboardID, int score)
    {
        Social.ReportScore(score, leaderboardID, success => { });
    }

    public static void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }

    #endregion



}
