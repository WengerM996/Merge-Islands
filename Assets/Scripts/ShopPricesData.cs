using System;
using UnityEngine;

public class ShopPricesData : MonoBehaviour
{
    [SerializeField] private Shop _shop;

    private string key = "ItemForSell";

    private void OnEnable()
    {
        ShopItem.PriceChanged += OnPriceChanged;
    }

    private void OnDisable()
    {
        ShopItem.PriceChanged -= OnPriceChanged;
    }

    private void Awake()
    {
        Load();
    }

    private void Load()
    {
        foreach (var item in _shop.ItemsForSell)
        {
            if (PlayerPrefs.HasKey(key + item.GetIndex()))
            {
                item.Price = PlayerPrefs.GetInt(key + item.GetIndex());
            }
        }
    }

    private void OnPriceChanged(int itemIndex, int value)
    {
        PlayerPrefs.SetInt(key + itemIndex, value);
        
        foreach (var item in _shop.ItemsForSell)
        {
            if (item.GetIndex() == itemIndex)
            {
                item.Price = value;
            }
        }
    }
}
