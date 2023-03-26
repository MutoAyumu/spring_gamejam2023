using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] int _characterID = -1;
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] float _stopInterval = 0.5f;
    [SerializeField] float _distance = 0.2f;
    [SerializeField] Vector2 _movePoint;

    Rigidbody2D _rb;

    public int CharacterID => _characterID;

    private void Awake()
    {
        TryGetComponent(out _rb);
    }

    private void Update()
    {
        OnMove();
    }

    void OnMove()
    {

    }

    void CoolTime()
    {

    }
}
