using System;
using DG.Tweening;
using UnityEngine;

public class FormAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _endPoint;
    [Space]
    [SerializeField] private Vector3 _endScale;
    [Space] 
    [SerializeField] private float _duration;

    private Tweener _moveTweener;
    private Tweener _scaleTweener;

    private void Awake()
    {
        Play();
    }

    private void Play()
    {
        _moveTweener = transform.DOMove(_endPoint, _duration);
        _scaleTweener = transform.DOScale(_endScale, _duration);
    }

    private void OnDestroy()
    {
        _moveTweener.Kill();
        _scaleTweener.Kill();
    }
}
