using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] int _characterID = -1;
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] float _stopInterval = 0.5f;
    [SerializeField] float _distance = 0.2f;
    [SerializeField] PointHandle _pointHandle;
    [SerializeField] StartEndPoint _movePoint;

    Rigidbody2D _rb;
    Transform _transform;
    Vector3 _nextPoint;
    float _timer;

    /// <summary>
    /// キャラクターの識別用ID
    /// </summary>
    public int CharacterID => _characterID;

    private void Awake()
    {
        TryGetComponent(out _rb);
        TryGetComponent(out _transform);

        var points = _pointHandle.MovePointArray;
        var r = Random.Range(0, points.Length);
        var point = points[r];
        _movePoint.Start = _transform.TransformPoint(point.Start);
        _movePoint.End = _transform.TransformPoint(point.End);
        _nextPoint = _movePoint.End;
        _transform.position = _movePoint.Start;
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    void OnMove()
    {
        var pos = _transform.position;

        if (Vector2.Distance(_nextPoint, pos) <= _distance)
        {
            _rb.velocity = Vector2.zero;
            CoolTime();
            return;
        }

        var dir = _nextPoint - pos;
        _rb.velocity = dir.normalized * _moveSpeed;

        Flip(dir.x);
    }

    void CoolTime()
    {
        _timer += Time.deltaTime;

        if (_timer >= _stopInterval)
        {
            _timer = 0;
            var point = _movePoint.Start;

            if (_nextPoint == _movePoint.Start)
            {
                point = _movePoint.End;
            }

            _nextPoint = point;
        }
    }

    void Flip(float h)
    {
        var scale = _transform.localScale;

        if(h >= 0)
        {
            _transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
        }
        else
        {
            _transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
        }
    }
}
