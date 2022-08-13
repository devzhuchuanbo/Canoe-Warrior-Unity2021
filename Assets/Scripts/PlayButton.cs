using System;
using UnityEngine;
//using GoogleMobileAds.Api;


public class PlayButton : MonoBehaviour
{
    //AD//
    //private InterstitialAd interstitial;
    //AD//
    private void Start()
    {
        RequestInterstitial();
    }

    private void LevelSellected(Transform t)
    {
        if (this.preTrans)
        {
            this.preTrans.gameObject.SendMessage("DeSellect", SendMessageOptions.DontRequireReceiver);
        }
        this.lvname = t.gameObject.name;
        string key = this.lvname + "Scroll";
        int @int = PlayerPrefs.GetInt(key);
        this.ScrollX3.gameObject.SetActive(true);
        this.ScrollX3.SendMessage("ShowStar", @int);
        this.preTrans = t;
    }

    private void LevelNotUnlock()
    {
        if (this.preTrans)
        {
            this.preTrans.gameObject.SendMessage("DeSellect", SendMessageOptions.DontRequireReceiver);
        }
        //this.ScrollX3.gameObject.SetActive(false);
    }
    //AD//
    private void RequestInterstitial()
    {
//#if UNITY_ANDROID
//        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
//#elif UNITY_IPHONE
//        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
//#else
//        string adUnitId = "unexpected_platform";
//#endif

        // Initialize an InterstitialAd.
        //this.interstitial = new InterstitialAd(adUnitId);

        //// Called when an ad request has successfully loaded.
        //this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        //// Called when an ad request failed to load.
        //this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        //// Called when an ad is shown.
        //this.interstitial.OnAdOpening += HandleOnAdOpened;
        //// Called when the ad is closed.
        //this.interstitial.OnAdClosed += HandleOnAdClosed;
        //// Called when the ad click caused the user to leave the application.
        //this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        //// Create an empty ad request.
        //AdRequest request = new AdRequest.Builder().Build();
        //// Load the interstitial with the request.
        //this.interstitial.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    //public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    //{
    //    MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
    //                        + args.Message);
    //}

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");

    }
    //AD//

    public void PlayLevelSellected()
    {
        //this.interstitial.Show();
        if (PlayerPrefs.GetInt("AdNumInt") == 3 && PlayerPrefs.GetInt("AdNumInt") != 1)
        {
            //if (this.interstitial.IsLoaded())
            //{

            //    this.interstitial.Show();
            //    PlayerPrefs.SetInt("AdNumInt", 0);
            //    LogIinterstitialShowEvent();
            //}
        }
        else
        {
            //PlayerPrefs.SetInt("AdNumInt", PlayerPrefs.GetInt("AdNumInt") + 1);
            print("IS       TWOOOO");
        }
        this.Loadding.gameObject.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(this.lvname);
    }
    public void LogIinterstitialShowEvent()
    {


    }

    public Transform ScrollX3;

    public Transform Loadding;

    private Transform preTrans;

    private string lvname;
}
