using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseIncome : MonoBehaviour
{
    [SerializeField] private FormDialog _formDialog;
    [SerializeField] private Accountant _accountant;
    [SerializeField] private float _duration;
    [SerializeField] private TMP_Text _viewCounter;

    private bool _adsIsWatched;
    private float _counter;
    private Button _button;

    private void OnEnable()
    {
       // FormDialog.UserWantsToDoubleIncome += OnUserWantsToDoubleIncome;
        MobAdsRewarded.RewardGranted += OnRewardGranted;
    }

    private void OnDisable()
    {
        //FormDialog.UserWantsToDoubleIncome -= OnUserWantsToDoubleIncome;
        MobAdsRewarded.RewardGranted -= OnRewardGranted;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void OnClick()
    {
        if (FormService.CurrentForm != null)
        {
            Destroy(FormService.CurrentForm);
        }
        
        FormService.CurrentForm = Instantiate(_formDialog).gameObject;
    }

    private void OnRewardGranted()
    {
        _button.interactable = false;
        _viewCounter.enabled = true;

        _accountant.Rate = 2;
        
        StartCoroutine(Counter());
    }

    private IEnumerator Counter()
    {
        _counter = _duration;
        
        while (_counter > 0)
        {
            _counter -= Time.deltaTime;
            UpdateCounterInfo();
            yield return new WaitForEndOfFrame();
        }

        _viewCounter.enabled = false;
        _button.interactable = true;
        
        _accountant.Rate = 1;
    }

    private void UpdateCounterInfo()
    {
        TimeSpan time = TimeSpan.FromSeconds(_counter);
        _viewCounter.text = time.ToString(@"mm\:ss");
    }
}
