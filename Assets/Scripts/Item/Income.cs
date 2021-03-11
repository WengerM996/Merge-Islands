using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Income : MonoBehaviour
{
    [SerializeField] private float _interval;
    [SerializeField] private int _valuePerInterval;
    private float _counter;

    public event UnityAction<int> CoinsSend;

    public int ValuePerInterval => _valuePerInterval;

    private void Update()
    {
        _counter += Time.deltaTime;

        if (_counter >= _interval)
        {
            _counter = 0;
            CoinsSend?.Invoke(_valuePerInterval);
            Accountant.AddCoins(_valuePerInterval);
        }
    }
}
