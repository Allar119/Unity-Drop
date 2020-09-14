using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public GameObject buttonOK;
    public GameObject buttonBack;


    // Use this for initialization
    void Start ()
    {
        if (0 == PlayerPrefs.GetInt("firstlaunch", 0))
        {
            buttonOK.SetActive(true);
            buttonBack.SetActive(false);
        }
        else
        {
            buttonOK.SetActive(false);
            buttonBack.SetActive(true);
        }
    }
}
