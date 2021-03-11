using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CommonAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _mergeSuccess;
    [SerializeField] private AudioClip _mergeFailed;
    [SerializeField] private AudioClip _unpack;
    [Space] 
    [SerializeField] private float _volumeSuccess;
    [SerializeField] private float _volumeFailed;
    [SerializeField] private float _volumeUnpack;

    private AudioSource _audioSource;
    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        ItemIntersection.MergeSuccess += OnMergeSuccess;
        ItemIntersection.MergeFailed += OnMergeFailed;
        Box.Unpacked += OnUnpacked;
    }

    private void OnDisable()
    {
        ItemIntersection.MergeSuccess -= OnMergeSuccess;
        ItemIntersection.MergeFailed -= OnMergeFailed;
        Box.Unpacked -= OnUnpacked;
    }

    private void OnMergeSuccess()
    {
        if (GeneralData.Sounds)
            _audioSource.PlayOneShot(_mergeSuccess, _volumeSuccess);
    }
    
    private void OnMergeFailed()
    {
        if (GeneralData.Sounds)
            _audioSource.PlayOneShot(_mergeFailed, _volumeFailed);
    }

    private void OnUnpacked(int index, Vector3 position)
    {
        if (GeneralData.Sounds)
            _audioSource.PlayOneShot(_unpack, _volumeUnpack);
    }
}
