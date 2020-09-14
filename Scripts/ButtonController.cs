using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public float delay = 0.5f;

    private GameObject LevelChanger;
    private Animator changeAnimator;
    private Animator canvisAnimator;


    void Awake()
    {        
        LevelChanger = GameObject.Find("LevelChanger");        
        changeAnimator = LevelChanger.GetComponent<Animator>();
    }

    //Loading new scene by index and with "fade" animation:
    public void LoadLevelWithFade(int levelIndex)
    {        
        StartCoroutine(PlayAnimtion(levelIndex));        
    }

    //Loading animation and new scene:
    public IEnumerator PlayAnimtion(int index)
    {
        changeAnimator.Play("Fade_out");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);
    }
    
    //Exit from game:
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }

    //Resets high scoor to zero:
    public void ResetProgress()
    {
        PlayerPrefs.SetInt("Best", 0);
        PlayerPrefs.SetInt("achievement", 0);
        PlayerPrefs.SetInt("firstlaunch", 0);
    }

    //Play audio clip on button bress
    public void PlayClick()
    {
        GetComponent<AudioSource>().Play();
    }

    //Open Google play for rating
    public void RateGame()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.alrgamesdev.Drop1");        
    }

    //Tutorial OK button controller
    public void TutorialOkButton()
    {
        if (0 == PlayerPrefs.GetInt("firstlaunch", 0))
        {
            PlayerPrefs.SetInt("firstlaunch", 1);
            LoadLevelWithFade(1);
        }
        else
        {
            LoadLevelWithFade(0);
        }
    }

    //Main Menu Play button controller
    public void MainMenuPlayButton()
    {
        if (0 == PlayerPrefs.GetInt("firstlaunch", 0))
        {            
            LoadLevelWithFade(2);
        }
        else
        {
            LoadLevelWithFade(1);
        }
    }

    //Main Menu Tutorial button controller
    public void MainMenuTutorialButton()
    {
        PlayerPrefs.SetInt("firstlaunch", 1);
        LoadLevelWithFade(2);
    }
    

    //Show leaderboard
    public void ShowLeaderboard()
    {
        PlayServices.ShowLeaderboardUI();
    }
}


