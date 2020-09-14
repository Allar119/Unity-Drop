using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Advertisements;
//using UnityEngine.Monetization;

public class UnityAdManage : MonoBehaviour {

    /*
    [SerializeField] private bool testMode = true;
    [SerializeField] private string gameId = "2867643";
    [SerializeField] private string placementId = "video";
    public int counter = 5;

    [Space]
    public TurnCounter turn;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("Ad Test mode = " + testMode);

        //Monetization.Initialize(gameId, testMode);
        Advertisement.Initialize(gameId, testMode);
    }


    public void ShowAd()
    {

        if (Advertisement.IsReady(placementId))
        {
            Advertisement.Show(placementId);
        }


        /*
        if (TurnCounter.turnCount == counter)
        {
            turn.TurnReset();
            StartCoroutine(ShowAdWhenReady());
        }

        
        else
        {
            ShowBannerAd();
        }
        */
    }

    /*
    //Show video ad:
    private IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show();
        }
    }
    */
    /*
    //Show banner ad:
    public void ShowBannerAd()
    {
        Debug.Log("Banner requested");
        Advertisement.Banner.Show();
    }

    //Hide banner ad:
    public void HideBannerAd()
    {
        Debug.Log("Banner destroyed");
        Advertisement.Banner.Hide(true);
    }    
    */

