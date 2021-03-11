using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsForm : MonoBehaviour
{
    [SerializeField] private Toggle _toggleSounds;
    [SerializeField] private Toggle _toggleMusic;


    private void Awake()
    {
        _toggleMusic.isOn = GeneralData.Music;
        _toggleSounds.isOn = GeneralData.Sounds;
    }

    public bool Music
    {
        get => BackgroundMusic.Enabled;
        set => BackgroundMusic.Enabled = value;
    }

    public bool Sounds
    {
        get
        { 
            return GeneralData.Sounds;
        }
        set
        {
            GeneralData.Sounds = value;
        }
    }

    public void OnClose()
    {
        Destroy(gameObject);
    }
}
