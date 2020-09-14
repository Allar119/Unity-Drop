using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackroundAudio : MonoBehaviour {

    void Awake()
    {
        int backroundAudioCount = FindObjectsOfType<BackroundAudio>().Length;
        if (backroundAudioCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        GetComponent<AudioSource>().Play();		
	}
}
