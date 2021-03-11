using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Accountant _accountant;
    [SerializeField] private ItemSpawner _itemSpawner;

    [SerializeField] private ShopForm _shopTemplate;
    
    [SerializeField] private ShopItem _shopItemTemplate;
    [SerializeField] private GameObject _lockItemTemplate;
    [SerializeField] private List<ItemExample> _itemsForSell;
    [Header("Ads")] [SerializeField] private MobAdsSimple _adsSimple;

    private Vector3 position;
    private AudioSource _audioSource;
    private float _startOffsetY = 1.5f;

    public List<ItemExample> ItemsForSell => _itemsForSell;

    private void Start()
    {
        position = transform.position;
    }

    public void OpenShop()
    {
        if (FormService.CurrentForm != null)
        {
            Destroy(FormService.CurrentForm);
        }
        
        var shop = Instantiate(_shopTemplate, position, Quaternion.identity, transform);
        FormService.CurrentForm = shop.gameObject;
        CreateElements(shop);
        
        _adsSimple.ShowAd();
    }

    private void CreateElements(ShopForm shop)
    {
        foreach (var item in _itemsForSell)
        {
            if (item.Unlock)
            {
                var shopItemObject = Instantiate(_shopItemTemplate, shop.ScrollView);
                var shopItem = shopItemObject.GetComponent<ShopItem>();
            
                shopItem.Accountant = _accountant;
                shopItem.ItemSpawner = _itemSpawner;
                shopItem.ItemExample = Instantiate(item, shopItem.GetItemPoint().position, Quaternion.identity, shopItemObject.transform);
            }
            else
            {
                Instantiate(_lockItemTemplate, shop.ScrollView);
            }
        }

    }
}
