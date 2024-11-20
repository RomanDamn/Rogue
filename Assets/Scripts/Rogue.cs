using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Rogue : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _baseOrderInLayer = 10;
    [SerializeField] private int _hiddenOrderInLayer = -1;
    [SerializeField] private Vector2 _targetPosition;

    private Vector2 _basePosition;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _basePosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    public void ShowByZ()
    {
        _spriteRenderer.sortingOrder = _baseOrderInLayer;
    }

    public void HideByZ()
    {
        _spriteRenderer.sortingOrder = _hiddenOrderInLayer;
    }

    private void Move()
    {
        var distance = Vector2.Distance(_targetPosition, transform.position);
        var isPointReached = distance <= 0.1;
        var step = _speed * Time.deltaTime;

        if (isPointReached)
        {
            var tempTargetPosition = _targetPosition;
            _targetPosition = _basePosition;
            _basePosition = tempTargetPosition;
            ToggleFlipX();
        }

        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, step);
    }

    private void ToggleFlipX()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}
