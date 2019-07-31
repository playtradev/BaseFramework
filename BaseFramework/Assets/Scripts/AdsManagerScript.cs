using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdsManagerScript : MonoBehaviour
{
   



	public static AdsManagerScript Instance;


	private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;

	private void Awake()
	{
		Instance = this;
	}
	// Start is called before the first frame update
	void Start()
    {
		#if UNITY_ANDROID
        string appId = "ca-app-pub-1267802095563724~6423421207";            //test
#elif UNITY_IPHONE
        string appId = "ca-app-pub-1267802095563724~9573835322";
#else
            string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
		RequestInterstitial();
        RequestVideoReward(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



	#region admob
  


    private void RequestVideoReward(bool firstTime)
    {
        #if UNITY_ANDROID
            //string adUnitId = "ca-app-pub-3940256099942544/5224354917";//test
            string adUnitId = "ca-app-pub-1267802095563724/9018604422";//real deal
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
        if(firstTime)
        {

			// Called when an ad request has successfully loaded.
			rewardBasedVideo.OnAdLoaded += RewardBasedVideo_OnAdLoaded;
            // Called when an ad request failed to load.
			rewardBasedVideo.OnAdFailedToLoad += RewardBasedVideo_OnAdFailedToLoad;
            // Called when an ad is shown.
			rewardBasedVideo.OnAdOpening += RewardBasedVideo_OnAdOpening;
            // Called when the ad starts to play.
			rewardBasedVideo.OnAdStarted += RewardBasedVideo_OnAdStarted;
            // Called when the user should be rewarded for watching a video.
			rewardBasedVideo.OnAdRewarded += RewardBasedVideo_OnAdRewarded;
            // Called when the ad is closed.
			rewardBasedVideo.OnAdClosed += RewardBasedVideo_OnAdClosed;
            // Called when the ad click caused the user to leave the application.
			rewardBasedVideo.OnAdLeavingApplication += RewardBasedVideo_OnAdLeavingApplication;

			rewardBasedVideo.OnAdCompleted+= RewardBasedVideo_OnAdCompleted;

        }
    }

	void RewardBasedVideo_OnAdLeavingApplication(object sender, System.EventArgs e)
    {
    }


    void RewardBasedVideo_OnAdCompleted(object sender, System.EventArgs e)
    {
    }


    void RewardBasedVideo_OnAdRewarded(object sender, Reward e)
    {
		//Reward
    }


    void RewardBasedVideo_OnAdStarted(object sender, System.EventArgs e)
    {
    }


    void RewardBasedVideo_OnAdClosed(object sender, System.EventArgs e)
    {
		RequestVideoReward(false);

    }


    void RewardBasedVideo_OnAdOpening(object sender, System.EventArgs e)
    {
    }


    void RewardBasedVideo_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
    }


    void RewardBasedVideo_OnAdLoaded(object sender, System.EventArgs e)
    {
    }

    private void RequestInterstitial()
    {
        
#if UNITY_ANDROID
        //string adUnitId = "ca-app-pub-3940256099942544/1033173712";     //test
        string adUnitId = "ca-app-pub-1267802095563724/5465562753";   //real deal
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Called when an ad request has successfully loaded.
		interstitial.OnAdLoaded += Interstitial_OnAdLoaded;;
        // Called when an ad request failed to load.
		interstitial.OnAdFailedToLoad += Interstitial_OnAdFailedToLoad;;
        // Called when an ad is shown.
		interstitial.OnAdOpening += Interstitial_OnAdOpening;;
        // Called when the ad is closed.
		interstitial.OnAdClosed += Interstitial_OnAdClosed;;
        // Called when the ad click caused the user to leave the application.
		interstitial.OnAdLeavingApplication += Interstitial_OnAdLeavingApplication;;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

	void Interstitial_OnAdLeavingApplication(object sender, System.EventArgs e)
    {
        interstitial.Destroy();
        RequestInterstitial();
    }


    void Interstitial_OnAdClosed(object sender, System.EventArgs e)
    {
        interstitial.Destroy();
        RequestInterstitial();
    }


    void Interstitial_OnAdOpening(object sender, System.EventArgs e)
    {
    }


    void Interstitial_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
    }


    void Interstitial_OnAdLoaded(object sender, System.EventArgs e)
    {
    }



    public void RewardAd()
    {
         if (rewardBasedVideo.IsLoaded())
         {
             rewardBasedVideo.Show();
         }
    }

    private void InsterstitialAd()
    {
        
        if (this.interstitial.IsLoaded() && PlayerPrefs.GetInt("Ads")==0) //ToDo use a variable
        {
            this.interstitial.Show();
        }
    }
    #endregion


}
