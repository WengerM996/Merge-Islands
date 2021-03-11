using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Accountant : MonoBehaviour
{
    [SerializeField] private ItemSpawner _itemSpawner;
    [SerializeField] private TMP_Text _viewSum;
    [SerializeField] private TMP_Text _viewIntervalSum;
    
    private int _intervalSum;
    private int _rate = 1;

    public static event UnityAction CoinsInfoUpdated;

    public int TotalSum
    {
        get => GeneralData.Coins;
        set => GeneralData.Coins = value;
    }

    public int Rate
    {
        get => _rate;
        set => _rate = value;
    }

    private void OnEnable()
    {
        CoinsInfoUpdated += OnCoinsInfoUpdated;
    }

    private void OnDisable()
    {
        CoinsInfoUpdated -= OnCoinsInfoUpdated;
    }

    private void Start()
    {
        UpdateView();
    }

    public static void AddCoins(int value)
    {
        GeneralData.Coins += value;
        CoinsInfoUpdated?.Invoke();
    }
    
    public void UpdateView()
    {
        _intervalSum = 0;
        
        foreach (var item in _itemSpawner.Items)
        {
            if (item != null)
            {
                var income = item.GetComponent<Income>();
                _intervalSum += income.ValuePerInterval;
            }
        }

        _intervalSum *= _rate;
        
        _viewIntervalSum.text = _intervalSum.ToString();
        _viewSum.text = GeneralData.Coins.ToString();
    }

    private void OnCoinsInfoUpdated()
    {
        UpdateView();
    }
}
