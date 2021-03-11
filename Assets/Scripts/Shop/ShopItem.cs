using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class ShopItem : MonoBehaviour
{
    [SerializeField] private Transform _itemPoint;
    [SerializeField] private TMP_Text _viewName;
    [SerializeField] private TMP_Text _viewPrice;
    [Space] [SerializeField] private AudioClip _itemPurchased;

    private ItemExample _itemExample;
    private Accountant _accountant;
    private ItemSpawner _itemSpawner;
    private AudioSource _audioSource;

    public static event UnityAction<int, int> PriceChanged; 

    public ItemExample ItemExample
    {
        get => _itemExample;
        set => _itemExample = value;
    }

    public Accountant Accountant
    {
        get => _accountant;
        set => _accountant = value;
    }

    public ItemSpawner ItemSpawner
    {
        get => _itemSpawner;
        set => _itemSpawner = value;
    }

    public Transform GetItemPoint()
    {
        return _itemPoint;
    }

    public TMP_Text ViewName
    {
        get => _viewName;
        set => _viewName.text = value.text;
    }

    public TMP_Text ViewPrice
    {
        get => _viewPrice;
        set => _viewPrice = value;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        _viewName.text = _itemExample.GetName();
        _viewPrice.text = _itemExample.Price.ToString();
    }

    public void Buying()
    {
        if (_accountant.TotalSum >= _itemExample.Price)
        {
            if (_itemSpawner.HasAvailableCell())
            {
                _itemSpawner.SpawnBox(_itemExample.GetIndex());
                
                _accountant.TotalSum -= _itemExample.Price;
                _accountant.UpdateView();

                _itemExample.Price = (int) (_itemExample.Price * 1.2f);
                UpdateInfo();
                
                PriceChanged?.Invoke(_itemExample.GetIndex(), _itemExample.Price);
                
                if (GeneralData.Sounds)
                    _audioSource.PlayOneShot(_itemPurchased);
            }
            
            
        }
    }
}
