using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;

public class MobAdsBanner : MonoBehaviour
{
    private BannerView _bannerView;

#if UNITY_ANDROID
    //private const string bannerUnitId = "ca-app-pub-3940256099942544/6300978111"; // ID for testing
    private const string bannerUnitId = "ca-app-pub-9228135477330812/1279551175"; // correct ID
#elif UNITY_IPHONE
    private const string bannerUnitId = "";
#else
    private const string bannerUnitId = "unexpected_platform";
#endif
    private void OnEnable()
    {
        _bannerView = new BannerView(bannerUnitId, AdSize.Banner, AdPosition.Bottom);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _bannerView.LoadAd(adRequest);
        StartCoroutine(ShowBanner());
    }

    private IEnumerator ShowBanner()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        _bannerView.Show();
    }
}
