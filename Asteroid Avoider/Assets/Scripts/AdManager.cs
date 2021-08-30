using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public static AdManager Instance;
    private GameOverHandler _gameOverHandler;
    [SerializeField] private bool testMode = true;
#if UNITY_ANDROID
    private string gameId = "4286893";
#elif UNITY_IOS
    private string gameId = "4286892";
#endif

    private void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, testMode);
        }
    }

    public void ShowAd(GameOverHandler gameOverHandler)
    {
        this._gameOverHandler = gameOverHandler;
        Advertisement.Show("rewardedVideo");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Unity ad ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("Unity ads error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Unity ad started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
                _gameOverHandler.ContinueGame();
                break;
            case ShowResult.Skipped:
                //Ad was skipped
                break;
            case ShowResult.Failed:
                Debug.LogWarning("Ad failed");
                break;
        }
    }
}