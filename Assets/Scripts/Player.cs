using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //[SerializeField] private InputAction _moveAction;
    [SerializeField] private InputAction _horizontalMoveAction;

    [SerializeField] private Rigidbody2D _rigidbody2d;
    [SerializeField] private float _speed;
    [SerializeField] private float _moveDirection;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
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
        // DontDestroyOnLoad(Instance);
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
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }
            _animator.SetBool("IsMove", true);
            transform.Translate(Vector2.right * _speed * _moveDirection * Time.deltaTime);
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