using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Box))]
public class BoxAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _positionTarget;
    [SerializeField] private Vector3 _rotationTarget;
    [SerializeField] private Vector3 _scaleTarget;
    [SerializeField] private float _duration;
    [SerializeField] private float _delay;

    private Tweener _moveTweener;
    private Tweener _scaleTweener;

    private void Start()
    {
        _moveTweener = transform.DOMove(transform.position + _positionTarget, _duration).SetLoops(2, LoopType.Yoyo).SetAutoKill(false);
        _scaleTweener = transform.DOScale(_scaleTarget, _duration).SetLoops(2, LoopType.Yoyo).SetAutoKill(false);
        
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);
        
            _moveTweener.Restart();
            _scaleTweener.Restart();
        }
    }

    private void OnDestroy()
    {
        _moveTweener.Kill();
        _scaleTweener.Kill();
    }
}
