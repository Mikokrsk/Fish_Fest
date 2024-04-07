using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private InputAction _moveAction;
    [SerializeField] private InputAction _horizontalMoveAction;

    [SerializeField] private Rigidbody2D _rigidbody2d;
    [SerializeField] private float _speed;
    [SerializeField] private float _moveDirection;
    [SerializeField] private Animator _animator;
    [SerializeField] public bool isMoving;

    [SerializeField] private int _money;
    public static Player Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(Instance);
    }
    private void Start()
    {
        _horizontalMoveAction.Enable();
    }

    void Update()
    {
        if (!isMoving)
        {
            return;
        }
        _moveDirection = _horizontalMoveAction.ReadValue<float>();
        if (_moveDirection != 0)
        {
            if (_moveDirection > 0)
            {
                transform.eulerAngles = new Vector3(0, 180f, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            _animator.SetBool("IsMove", true);
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool("IsMove", false);
        }
    }

    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
        }
    }

}