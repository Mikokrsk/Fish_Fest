using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hook : MonoBehaviour
{
    [SerializeField] private InputAction _verticalMoveAction;
    [SerializeField] private Rigidbody2D _rigidbody2d;
    [SerializeField] private GameObject _ship;
    [SerializeField] private float _moveDirection;
    [SerializeField] private float _speed;
    [SerializeField] private float _topEdge;
    [SerializeField] private float _downEdge;
    [SerializeField] private float _fishingLineLength;
    [SerializeField] private GameObject _fish;
    void Start()
    {
        _verticalMoveAction.Enable();
        _topEdge = _ship.transform.position.y;
        _downEdge = _ship.transform.position.y - _fishingLineLength;
    }

    void Update()
    {
        _moveDirection = _verticalMoveAction.ReadValue<float>();
        _downEdge = _ship.transform.position.y - _fishingLineLength;
    }

    private void FixedUpdate()
    {
        var positionY = _rigidbody2d.position.y + _moveDirection * _speed * Time.deltaTime;
        if (_fish != null)
        {
            _fish.transform.position = transform.position;
        }
        if (positionY >= _topEdge)
        {
            positionY = _topEdge;
            if (_fish != null)
            {
                var fish = _fish.GetComponent<Fish>();
                Destroy(_fish);
                Debug.Log($"{fish.fishAsset.fishName}  {fish.fishAsset.weight} {fish.fishAsset.cost}");
            }
        }
        if (positionY <= _downEdge)
        {
            positionY = _downEdge;
        }

        Vector2 movePosition = new Vector2(_ship.transform.position.x, positionY);
        _rigidbody2d.MovePosition(movePosition);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_fish == null)
        {
            var fish = collision.GetComponent<Fish>();
            if (fish != null)
            {
                _fish = fish.CaughtFish();
                Debug.Log($"{fish.fishAsset.fishName}  {fish.fishAsset.weight} {fish.fishAsset.cost}");
            }
        }
    }
}