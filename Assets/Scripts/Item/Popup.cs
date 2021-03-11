using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private GameObject _popupPart;
    [SerializeField] private TMP_Text _viewText;
    [SerializeField] private Image _logoView;

    private Tweener _popupTweener;
    private Tweener _textTweener;
    private Tweener _logoTweener;

    public void Action(float end, float duration, string text)
    {
        _viewText.text = text;
        _popupTweener = _popupPart.transform.DOMoveY(end, duration);
        _textTweener = _viewText.DOFade(0, duration);
        _logoTweener = _logoView.DOFade(0, duration);
        
        Destroy(gameObject, duration);
    }

    private void OnDestroy()
    {
        _popupTweener.Kill();
        _textTweener.Kill();
        _logoTweener.Kill();
    }
}
