using System;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private Rogue _objectToDetect;

    public Action<Rogue> Detected;
    public Action<Rogue> Leaved;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(ReferenceEquals(collision.gameObject, _objectToDetect.gameObject))
        {
            Detected?.Invoke(_objectToDetect);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(ReferenceEquals(collision.gameObject, _objectToDetect.gameObject))
        {
            Leaved?.Invoke(_objectToDetect);
        }
    }
}
