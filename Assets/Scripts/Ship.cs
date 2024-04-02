using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Ship : MonoBehaviour
{
    [SerializeField] private InputAction _moveAction;
    [SerializeField] private InputAction _horizontalMoveAction;
    [SerializeField] private Vector2 _move;

    // [SerializeField] private Vector2 _moveDirection = new Vector2(1, 0);

    [SerializeField] private Rigidbody2D _rigidbody2d;
    [SerializeField] private float _speed;
    [SerializeField] private float _moveDirection;
    [SerializeField] private float _leftEdge;
    [SerializeField] private float _rightEdge;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        _horizontalMoveAction.Enable();
    }

    void Update()
    {
        _moveDirection = _horizontalMoveAction.ReadValue<float>();
        _move = _moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        var positionX = _rigidbody2d.position.x + _moveDirection * _speed * Time.deltaTime;
        if (positionX <= _leftEdge)
        {
            positionX = _leftEdge;
        }
        if (positionX >= _rightEdge)
        {
            positionX = _rightEdge;
        }

        Vector2 movePosition = new Vector2(positionX, transform.position.y);
        _rigidbody2d.MovePosition(movePosition);
    }
}