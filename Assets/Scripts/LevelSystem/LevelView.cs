using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LevelSystem))]
public class LevelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _viewProgress;
    [SerializeField] private TMP_Text _viewLevel;
    [SerializeField] private Image _progressBar;
    [Header("Experience Bar Animation")] 
    [SerializeField] private GameObject _viewBar;
    [SerializeField] private Vector3 _scaleBar;
    [SerializeField] private float _durationBar;
    [Header("Level View Animation")] 
    [SerializeField] private GameObject _level;
    [SerializeField] private Vector3 _scaleLevel;
    [SerializeField] private float _durationLevel;

    private LevelSystem _levelSystem;

    private void Awake()
    {
        _levelSystem = GetComponent<LevelSystem>();
    }

    private void Start()
    {
        OnUpdated();
    }

    private void OnEnable()
    {
        _levelSystem.Updated += OnUpdated;
        _levelSystem.ReachedNextLevel += OnReachedNextLevel;
    }

    private void OnDisable()
    {
        _levelSystem.Updated -= OnUpdated;
        _levelSystem.ReachedNextLevel -= OnReachedNextLevel;
    }

    private void OnUpdated()
    {
        _viewProgress.text = _levelSystem.Progress + " / " + _levelSystem.LeftToNext;
        _viewLevel.text = _levelSystem.Level.ToString();
        
        _progressBar.fillAmount = (float) _levelSystem.Progress / _levelSystem.LeftToNext;

        _viewBar.transform.DOScale(_scaleBar, _durationBar).SetLoops(2, LoopType.Yoyo);
    }

    private void OnReachedNextLevel()
    {
        _level.transform.DOScale(_scaleLevel, _durationLevel).SetLoops(2, LoopType.Yoyo);
    }
}
