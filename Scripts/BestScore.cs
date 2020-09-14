using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour {

    public Text bestScore;
	
	// Update is called once per frame
	void Update ()
    {
        bestScore.text = PlayerPrefs.GetInt("Best", 0).ToString();
    }
}
