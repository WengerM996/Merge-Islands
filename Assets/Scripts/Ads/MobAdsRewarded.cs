using UnityEngine;

using GoogleMobileAds.Api;
using UnityEngine.Events;

public class MobAdsRewarded : MonoBehaviour
{
    private RewardedAd _rewardedAd;

    public static event UnityAction RewardGranted;

#if UNITY_ANDROID
    //private const string rewardedUnitId = "ca-app-pub-3940256099942544/5224354917"; // test ID
    private const string rewardedUnitId = "ca-app-pub-9228135477330812/6079811083"; // correct ID
#elif UNITY_IPHONE
    private const string rewardedUnitId = "";
#else
    private const string rewardedUnitId = "unexpected_platform";
#endif
    private void OnEnable()
    {
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        FormDialog.UserWantsToDoubleIncome += OnUserWantsToDoubleIncome;
    }

    private void OnDisable()
    {
        _rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
        FormDialog.UserWantsToDoubleIncome -= OnUserWantsToDoubleIncome;
    }

    private void Awake()
    {
        LoadAds();
    }

    private void LoadAds()
    {
        _rewardedAd = new RewardedAd(rewardedUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(adRequest);
    }
    
    private void OnUserWantsToDoubleIncome()
    {
        ShowRewardedAd();
    }

    private void ShowRewardedAd()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show(); 
            LoadAds();
        }
    }

    private void HandleUserEarnedReward(object sender, Reward args)
    {
        RewardGranted?.Invoke();
    }
}
