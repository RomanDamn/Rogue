using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectDetector))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _targetVolume = 1f;
    [SerializeField] private float _volumeChangeSpeed = 10f;

    private float _currentVolume;
    private ObjectDetector _detector;
    private Coroutine _coroutine;
    private float _volumeAccuracy = 0.1f;

    private void OnEnable()
    {
        _detector = GetComponent<ObjectDetector>();
        _detector.Detected += HandleDetection;
        _detector.Leaved += HandleLeave;
    }

    private void OnDisable()
    {
        _detector.Detected -= HandleDetection;
        _detector.Leaved -= HandleLeave;
    }   

    private void HandleDetection(Rogue rogue)
    {
        rogue.HideByZ();
        SetDefaultVolume();
        _audioSource.Play();
        StopRunningCoroutine();
        _coroutine = StartCoroutine(IncreaseVolume());
    }

    private void HandleLeave(Rogue rogue)
    {
        rogue.ShowByZ();
        StopRunningCoroutine();
        _coroutine = StartCoroutine(DecreaseVolume());
    }

    private IEnumerator IncreaseVolume()
    {
        while (_targetVolume > _currentVolume)
        {
            _currentVolume = Mathf.Lerp(_currentVolume, _targetVolume, _volumeChangeSpeed * Time.deltaTime);
            _audioSource.volume = _currentVolume;
            yield return null;
        }
    }

    private IEnumerator DecreaseVolume()
    {
        float targetVolume = 0;

        while (_currentVolume > _volumeAccuracy)
        {
            _currentVolume = Mathf.Lerp(_currentVolume, targetVolume, _volumeChangeSpeed * Time.deltaTime);
            _audioSource.volume = _currentVolume;
            yield return null;
        }

        SetDefaultVolume();
    }

    private void SetDefaultVolume()
    {
        _currentVolume = 0;
        _audioSource.volume = _currentVolume;
    }

    private void StopRunningCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }
}
