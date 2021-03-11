using UnityEngine;

[RequireComponent(typeof(Income))]
[RequireComponent(typeof(ItemIntersection))]
[RequireComponent(typeof(AudioSource))]
public class ItemAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _arrival;

    private AudioSource _arrivalSound;
    private Income _income;

    private void Awake()
    {
        _income = GetComponent<Income>();
        _arrivalSound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _income.CoinsSend += OnCoinsSend;
    }

    private void OnDisable()
    {
        _income.CoinsSend -= OnCoinsSend;
    }

    private void OnCoinsSend(int value)
    {
        if (GeneralData.Sounds)
            _arrivalSound.PlayOneShot(_arrival, 0.2f);
    }
}
