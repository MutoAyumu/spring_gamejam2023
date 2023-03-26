using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] int _characterID = -1;
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] float _stopInterval = 0.5f;
    [SerializeField] float _distance = 0.2f;
    //[SerializeField] Vector2 _movePoint;
    [SerializeField] Transform[] _test;

    Rigidbody2D _rb;
    Transform _transform;
    int _movePointIndex;
    float _timer;

    /// <summary>
    /// キャラクターの識別用ID
    /// </summary>
    public int CharacterID => _characterID;

    private void Awake()
    {
        TryGetComponent(out _rb);
        TryGetComponent(out _transform);
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    void OnMove()
    {
        var pos = (Vector2)_transform.position;

        if (Vector2.Distance(_test[_movePointIndex].position, pos) <= _distance)
        {
            _rb.velocity = Vector2.zero;
            CoolTime();
            return;
        }

        var dir = (Vector2)_test[_movePointIndex].position - pos;
        _rb.velocity = dir.normalized * _moveSpeed;
    }

    void CoolTime()
    {
        _timer += Time.deltaTime;

        if(_timer >= _stopInterval)
        {
            _timer = 0;

            var index = (_movePointIndex + 1) % _test.Length;
            _movePointIndex = index;
        }
    }
}
