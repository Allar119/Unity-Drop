using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarUnloacker : MonoBehaviour {

    public Image star_5;
    public Image star_25;
    public Image star_50;
    public Image star_75;


	// Use this for initialization
	void Start ()
    {
        StarUnlock(star_5, 5);
        StarUnlock(star_25, 25);
        StarUnlock(star_50, 50);
        StarUnlock(star_75, 75);
    }
	
    void StarUnlock(Image starName, int starValu)
    {
        if (PlayerPrefs.GetInt("Best", 0) >= starValu)
        {
            starName.color = new Color32(226, 202, 120, 255);
        }
    }

}
