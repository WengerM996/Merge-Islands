using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpawnButton))]
public class SpawnButtonAnimation : MonoBehaviour
{
    [SerializeField] private Image _viewSpawnProgress;
    [SerializeField] private Vector3 _scaleAfterClick;
    [SerializeField] private float _duration;
    
    private SpawnButton _spawnButton;
    private Tweener _scaleTweener;

    private void Awake()
    {
        _spawnButton = GetComponent<SpawnButton>();
    }

    private void OnEnable()
    {
        _spawnButton.ViewCounterUpdated += OnViewCounterUpdated;
    }

    private void OnDisable()
    {
        _spawnButton.ViewCounterUpdated -= OnViewCounterUpdated;
    }

    public void OnButtonClick()
    {
        if (_scaleTweener == null)
        {
            _scaleTweener = transform.DOScale(_scaleAfterClick, _duration).SetLoops(2, LoopType.Yoyo).SetAutoKill(false);
        }
        else
        {
            _scaleTweener.Restart();
        }
        
    }

    private void OnViewCounterUpdated(float value)
    {
        const float full_Fill_Amount = 1f;
        var amount = full_Fill_Amount - value / _spawnButton.MaxCounterValue;
        _viewSpawnProgress.fillAmount = amount;
    }
}
