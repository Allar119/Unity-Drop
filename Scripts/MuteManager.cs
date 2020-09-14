using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MuteManager : MonoBehaviour {

    public GameObject muteButton;
    public GameObject unmuteButton;
    public AudioMixer audioMixer;

    void Start()
    {          
        if (PlayerPrefs.GetInt("Volume", 0) == 1)
        {
            audioMixer.SetFloat("Volume", -80);
            muteButton.SetActive(false);
            unmuteButton.SetActive(true);
        }

        else if (PlayerPrefs.GetInt("Volume", 0) == 0)
        {
            audioMixer.SetFloat("Volume", 0);
            muteButton.SetActive(true);
            unmuteButton.SetActive(false);
        }
    }

    //Mute audio (master mixer volume)
    public void MuteAudio()
    {
        //-80 = audio muted
        PlayerPrefs.SetInt("Volume", 1);
        audioMixer.SetFloat("Volume", -80);
        muteButton.SetActive(false);
        unmuteButton.SetActive(true);
    }

    //Mute audio (master mixer volume)
    public void UnmuteAudio()
    {
        // 0 = audio not muted
        PlayerPrefs.SetInt("Volume", 0);
        audioMixer.SetFloat("Volume", 0);
        unmuteButton.SetActive(false);
        muteButton.SetActive(true);
    }

}
