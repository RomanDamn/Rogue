using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioAlarm;
    [SerializeField] private float _targetVolume = 1f;
    [SerializeField] private float _volumeChangeSpeed = 10f;

    private float _currentVolume;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Rogue rogue))
        {
            rogue.HideByZ();
            SetDefaultVolume();
            _audioAlarm.Play();
            StopAllCoroutines();
            StartCoroutine(IncreaseVolume());
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Rogue rogue))
        {
            rogue.ShowByZ();
            StopAllCoroutines();
            StartCoroutine(DecreaseVolume());
        }
    }

    private IEnumerator IncreaseVolume()
    {
        while (_targetVolume > _currentVolume)
        {
            _currentVolume = Mathf.Lerp(_currentVolume, _targetVolume, _volumeChangeSpeed * Time.deltaTime);
            _audioAlarm.volume = _currentVolume;
            yield return null;
        }
    }

    private IEnumerator DecreaseVolume()
    {
        while (_currentVolume > 0.1)
        {
            _currentVolume = Mathf.Lerp(_currentVolume, 0, _volumeChangeSpeed * Time.deltaTime);
            _audioAlarm.volume = _currentVolume;
            yield return null;
        }

        SetDefaultVolume();
    }

    private void SetDefaultVolume()
    {
        _currentVolume = 0;
        _audioAlarm.volume = _currentVolume;
    }
}
