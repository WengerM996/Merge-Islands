using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    private AudioSource _audioSource;

    public static event UnityAction MusicSettingsChanged;

    public static bool Enabled
    {
        get
        {
            return GeneralData.Music;
        }
        set
        {
            GeneralData.Music = value;
            MusicSettingsChanged?.Invoke();
        } 
    }

    private void OnEnable()
    {
        MusicSettingsChanged += Play;
    }

    private void OnDisable()
    {
        MusicSettingsChanged -= Play;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        Play();
    }

    public void Play()
    {
        if (GeneralData.Music)
        {
            _audioSource.clip = _clip;
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }
}
