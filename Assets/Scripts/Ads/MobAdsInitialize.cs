using UnityEngine;

using GoogleMobileAds.Api;

public class MobAdsInitialize : MonoBehaviour
{
    private void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }
}
