using System;
using UnityEngine;

public class ShopData : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private Unlock _unlock;

    private string _key = "ShopItem";

    private void Awake()
    {
        Load();
    }

    private void OnEnable()
    {
        _unlock.UnlockedNewItem += OnUnlockedNewItem;
    }

    private void OnDisable()
    {
        _unlock.UnlockedNewItem -= OnUnlockedNewItem;
    }

    private void Load()
    {
        foreach (var item in _shop.ItemsForSell)
        {
            if (PlayerPrefs.HasKey(_key + item.GetIndex()))
            {
                item.Unlock = Convert.ToBoolean(PlayerPrefs.GetInt(_key + item.GetIndex()));
            }
        }
    }
    
    private void OnUnlockedNewItem(int itemIndex)
    {
        Save(itemIndex, 1);
    }

    private void Save(int itemIndex, int unlockStatus)
    {
        PlayerPrefs.SetInt(_key + itemIndex, unlockStatus);
    }
}
