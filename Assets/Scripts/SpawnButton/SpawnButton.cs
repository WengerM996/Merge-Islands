using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SpawnButton : MonoBehaviour
{
    [SerializeField] private float _maxCounterValue;
    [SerializeField] private TMP_Text _viewCounter;
    [SerializeField] private ItemSpawner _itemSpawner;
    [Space] 
    [SerializeField] private AudioClip _click;

    private Button _button;
    private float _counter;
    private bool _counterEnabled;
    private AudioSource _audioSource;

    public event UnityAction<float> ViewCounterUpdated;

    public float MaxCounterValue => _maxCounterValue;

    private void OnEnable()
    {
        _itemSpawner.FieldIsFull += OnFieldIsFull;
        _itemSpawner.FieldHasEmptyCell += OnFieldHasEmptyCell;
    }

    private void OnDisable()
    {
        _itemSpawner.FieldIsFull -= OnFieldIsFull;
        _itemSpawner.FieldHasEmptyCell -= OnFieldHasEmptyCell;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _counterEnabled = true;
        _counter = _maxCounterValue;
    }

    private void Update()
    {
        RunCounter();
        
        if (_counterEnabled)
        {
            
            UpdateViewSpawnCounter(Convert.ToInt32(_counter).ToString());
        }
    }

    private void RunCounter()
    {
        if (!_counterEnabled) return;
        
        _counter -= Time.deltaTime;
        
        if (_counter <= 0)
        {
            _counter = _maxCounterValue;
            _itemSpawner.SpawnBox();
        }
    }
    
    public void OnButtonClick()
    {
        _counter -= 1;
        if (GeneralData.Sounds)
            _audioSource.PlayOneShot(_click);
    }
    
    private void UpdateViewSpawnCounter(string value)
    {
        ViewCounterUpdated?.Invoke(_counter);
        _viewCounter.text = value;
    }

    private void OnFieldIsFull()
    {
        _button.interactable = false;
        _counterEnabled = false;
        UpdateViewSpawnCounter("Full");
    }

    private void OnFieldHasEmptyCell()
    {
        if (_counterEnabled) return;
        
        _button.interactable = true;
        _counterEnabled = true;
    }
}
