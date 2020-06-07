using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

using UnityEngine.SceneManagement;

public class AdManage : MonoBehaviour,IUnityAdsListener
{
    private string playStoreID = "3501181"; // just for googleplay
    private string interstitialAd = "video";
    private string rewardedVideoAd = "rewardedVideo";

    public bool isTargetPlayStore;
    public bool isTestAd;

    private void Start()
    {
        Advertisement.AddListener(this);
        InitializeAdvertisement(); //need internet
    }
    private void InitializeAdvertisement()
    {
        if (isTargetPlayStore)
        {
            Advertisement.Initialize(playStoreID, isTestAd); return;
        }
    }
    public void PlayInterstitialAd()
    {
        if (!Advertisement.IsReady(interstitialAd))
        {
            SceneManager.LoadScene("GameScene"); // if no internet
            return;
        }
        Advertisement.Show(interstitialAd);
    }

    public void RewardedVideoAd()
    {
        if (!Advertisement.IsReady(rewardedVideoAd))
            return;
        Advertisement.Show(rewardedVideoAd);

    }
    public void OnUnityAdsDidError(string message)
    {
        // throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed: Debug.Log("faild"); break;
            case ShowResult.Skipped:
                if (placementId == interstitialAd)
                    SceneManager.LoadScene("GameScene"); break;
            case ShowResult.Finished:
                if (placementId == rewardedVideoAd)
                    Debug.Log("reward player");
                if (placementId == interstitialAd)
                    SceneManager.LoadScene("GameScene");
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //  throw new System.NotImplementedException();
    }

    public void OnUnityAdsReady(string placementId)
    {
        //  throw new System.NotImplementedException();
    }



}
