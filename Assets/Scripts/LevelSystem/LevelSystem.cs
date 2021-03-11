using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class LevelSystem : MonoBehaviour
{
    [SerializeField] private ItemSpawner _itemSpawner;
    [SerializeField] private float _rateForNextLevel;
    [Space] [SerializeField] private AudioClip _levelUp;

    private AudioSource _audioSource;

    public event UnityAction ReachedNextLevel;
    public event UnityAction Updated;

    public int Level => GeneralData.Level;

    public int Progress => GeneralData.Experience;

    public int LeftToNext => GeneralData.ExperienceLeft;

    private void OnEnable()
    {
        _itemSpawner.SuccessMerge += OnSuccessMerge;
    }

    private void OnDisable()
    {
        _itemSpawner.SuccessMerge -= OnSuccessMerge;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnSuccessMerge(int progress)
    {
        GeneralData.Experience += progress;

        if (GeneralData.Experience >= GeneralData.ExperienceLeft)
        {
            NextLevel();
        }
        
        Updated?.Invoke();
    }

    private void NextLevel()
    {
        GeneralData.Experience = 0;
        GeneralData.ExperienceLeft = (int) (GeneralData.ExperienceLeft * _rateForNextLevel);
        GeneralData.Level++;
        
        ReachedNextLevel?.Invoke();
        if (GeneralData.Sounds)
            _audioSource.PlayOneShot(_levelUp);
    }
}
