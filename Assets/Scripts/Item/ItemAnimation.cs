using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Income))]
public class ItemAnimation : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private Vector3 _scaleTarget;
    [SerializeField] private float _duration;

    [Header("Popup")]
    // [SerializeField] private GameObject _popupPart;
    // [SerializeField] private TMP_Text _popupText;
    // [SerializeField] private Image _popupImage;

    [SerializeField]
    private Popup _popupTemplate;
    
    [SerializeField] private float _popupEndPoint;
    [SerializeField] private float _popupDuration;
    
    private float _popupDirection;
    //private Vector3 _startPositionPopupPart;
    
    private Income _income;
    private Tweener _itemTweener;
    private Tweener _popupTweener;
    private Tweener _fadeTextTweener;
    private Tweener _fadeLogoTweener;

    private void OnEnable()
    {
        _income.CoinsSend += OnCoinsSend;
    }

    private void OnDisable()
    {
        _income.CoinsSend -= OnCoinsSend;
    }

    private void Awake()
    {
        _income = GetComponent<Income>();
        InitValues();
    }

    public void SetScale(Vector3 newScale)
    {
        if (_itemTweener == null)
        {
            _itemTweener = transform.DOScale(newScale, _duration).SetLoops(2, LoopType.Yoyo).SetAutoKill(false);
        }
        else
        {
            _itemTweener.ChangeEndValue(newScale);
            _itemTweener.Restart();
        }
    }

    public void SetDefaultScale()
    {
        _itemTweener?.ChangeEndValue(_scaleTarget);
    }

    private void InitValues()
    {
        _popupDirection = transform.position.y;
        _popupDirection += _popupEndPoint;
        //_startPositionPopupPart = _popupPart.transform.position;
    }

    private void OnCoinsSend(int value)
    {
        ChangeScale();
        ShowPopupText(value);
    }
    
    private void ChangeScale()
    {
        if (_itemTweener == null)
        {
            _itemTweener = transform.DOScale(_scaleTarget, _duration).SetLoops(2, LoopType.Yoyo).SetAutoKill(false);
        }
        else
        {
            _itemTweener.Restart();
        }
    }

    private void ShowPopupText(int value)
    {
        _popupDirection = transform.position.y + _popupEndPoint;
        var popup = Instantiate(_popupTemplate, transform.position, Quaternion.identity, transform.parent);
        popup.Action(_popupDirection, _popupDuration, value.ToString());

        /*if (_popupTweener == null)
        {
            _popupTweener = _popupPart.transform
                .DOMoveY(_popupDirection, _popupDuration)
                .SetLoops(1, LoopType.Restart)
                .SetAutoKill(false);
            
            //_popupPart.transform.SetParent(transform.parent);
        }
        else
        {
            _popupDirection = _startPositionPopupPart.y + _popupEndPoint;
            var endPosition = new Vector3(0f, _popupDirection, 0f);
            _popupTweener.ChangeValues(_startPositionPopupPart, endPosition);
            _popupTweener.Restart();
        }

        if (_fadeTextTweener == null)
        {
            _fadeTextTweener = _popupText.DOFade(0, _popupDuration).SetLoops(1, LoopType.Restart).SetAutoKill(false);
            _fadeLogoTweener = _popupImage.DOFade(0, _popupDuration).SetLoops(1, LoopType.Restart).SetAutoKill(false);
        }
        else
        {
            _fadeTextTweener.Restart();
            _fadeLogoTweener.Restart();
        }*/

    }

    private void OnDestroy()
    {
        _itemTweener.Kill();
        _popupTweener.Kill();
        _fadeTextTweener.Kill();
        _fadeLogoTweener.Kill();
    }
}
