//using GoogleMobileAds.Api;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class InitializeAds : MonoBehaviour {


//    // Initialize an InterstitialAd.
//    string adUnitId = "ca-app-pub-3940256099942544/4411468910";
//    InterstitialAd interstitial;

//    // Use this for initialization
//    public void Start()
//    {
//#if UNITY_ANDROID
//        string appId = "ca-app-pub-5376766505062595~5016030605";
//#elif UNITY_IPHONE
//            string appId = "ca-app-pub-3940256099942544~1458002511";
//#else
//            string appId = "unexpected_platform";
//#endif
//        interstitial = new InterstitialAd(adUnitId);
//        // Initialize the Google Mobile Ads SDK.
//        MobileAds.Initialize(appId);
//        RequestInterstitial();
//    }

//    // Update is called once per frame
//    void Update () {
		
//	}

//    private void RequestInterstitial()
//    {


//        // Called when an ad request has successfully loaded.
//        interstitial.OnAdLoaded += HandleOnAdLoaded;
//        // Called when an ad request failed to load.
//        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
//        // Called when an ad is shown.
//        interstitial.OnAdOpening += HandleOnAdOpened;
//        // Called when the ad is closed.
//        interstitial.OnAdClosed += HandleOnAdClosed;
//        // Called when the ad click caused the user to leave the application.
//        interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

//        // Create an empty ad request.
//        AdRequest request = new AdRequest.Builder().Build();
//        // Load the interstitial with the request.
//        interstitial.LoadAd(request);
//    }
//        public void HandleOnAdLoaded(object sender, EventArgs args)
//        {
//            MonoBehaviour.print("HandleAdLoaded event received");
//        }

//        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//        {
//            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
//                                + args.Message);
//        }

//        public void HandleOnAdOpened(object sender, EventArgs args)
//        {
//            MonoBehaviour.print("HandleAdOpened event received");
//        }

//        public void HandleOnAdClosed(object sender, EventArgs args)
//        {
//            MonoBehaviour.print("HandleAdClosed event received");
//        }

//        public void HandleOnAdLeavingApplication(object sender, EventArgs args)
//        {
//            MonoBehaviour.print("HandleAdLeavingApplication event received");
//        }
    


//    public void showInterstitialAd()
//    {
//        if (interstitial.IsLoaded())
//        {
//            interstitial.Show();
//        }
//    }
//}
