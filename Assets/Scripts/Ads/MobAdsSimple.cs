using System.Collections;
using UnityEngine;

using GoogleMobileAds.Api;

public class MobAdsSimple : MonoBehaviour
{
    [SerializeField] private float _interval;
    private InterstitialAd interstitialAd;
    private float _counter;
    private bool _adsAllowed;

#if UNITY_ANDROID
    //private const string interstitialUnitId = "ca-app-pub-3940256099942544/8691691433"; // test ID
    private const string interstitialUnitId = "ca-app-pub-9228135477330812/8278227886"; // correct ID
#elif UNITY_IPHONE
    private const string interstitialUnitId = "";
#else
    private const string interstitialUnitId = "unexpected_platform";
#endif
    private void OnEnable()
    {
        LoadAds();
    }

    private void Awake()
    {
        StartCoroutine(Counter());
    }

    private void LoadAds()
    {
        interstitialAd = new InterstitialAd(interstitialUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }

    private IEnumerator Counter()
    {
        _counter = 0;
        _adsAllowed = false;
        
        while (_counter < _interval)
        {
            _counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _adsAllowed = true;
    }

    public void ShowAd()
    {
        if (interstitialAd.IsLoaded() && _adsAllowed)
        {
            interstitialAd.Show();
            LoadAds();
            StartCoroutine(Counter());
        }
    }
}
