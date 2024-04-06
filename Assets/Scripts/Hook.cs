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
    public GameObject fishOnHook;
    [SerializeField] private FishSpawnManager _fishSpawnManager;
    public bool isMove;
    public static Hook Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        _verticalMoveAction.Enable();
        _topEdge = _ship.transform.position.y;
        _downEdge = _ship.transform.position.y - _fishingLineLength;
    }

    void Update()
    {
        _moveDirection = _verticalMoveAction.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        if (!isMove)
        {
            return;
        }
        var positionY = _rigidbody2d.position.y + _moveDirection * _speed * Time.deltaTime;
        if (fishOnHook != null)
        {
            fishOnHook.transform.position = transform.position;
        }
        if (positionY >= _topEdge)
        {
            positionY = _topEdge;
            if (fishOnHook != null)
            {
                var fish = fishOnHook.GetComponent<Fish>();
                _fishSpawnManager.RemoveFish(fish.gameObject);
                Destroy(fishOnHook);
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
        if (fishOnHook == null)
        {
            var fish = collision.GetComponent<Fish>();
            if (fish != null)
            {
                fishOnHook = fish.gameObject;
                FishingManager.Instance.StartFishing(fish);
            }
        }
    }
}