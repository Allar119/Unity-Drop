using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NaviBar : MonoBehaviour {

    public GameObject levelChanger;    
    ButtonController buttonControllScript;
    
    // Use this for initialization
    void Start ()
    {
        Screen.fullScreen = false;
        buttonControllScript = levelChanger.GetComponent<ButtonController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Debug.Log("Exit");
                Application.Quit();
            }

            else
            {
                Debug.Log("Back");
                buttonControllScript.LoadLevelWithFade(0);
            }
        }
    }
}