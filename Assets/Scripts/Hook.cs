using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Hook : MonoBehaviour
{
    [SerializeField] private InputAction _verticalMoveAction;
    [SerializeField] private InputAction _horizontalMoveAction;
    [SerializeField] private InputAction _moveAction;
    [SerializeField] private Rigidbody2D _rigidbody2d;
    [SerializeField] private GameObject _ship;
    [SerializeField] private Transform _shipModelTransform;
    [SerializeField] private Vector2 _moveVector;
    [SerializeField] private float _hookSpeed;
    [SerializeField] private float _shipSpeed;
    [SerializeField] private float _topEdge;
    [SerializeField] private float _downEdge;
    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private float _fishingLineLength;
    public GameObject fishOnHook;
    [SerializeField] private FishSpawnManager _fishSpawnManager;

    [SerializeField] private List<HooksAndRadius> _hooksAndRadiusList;
    [SerializeField] private int _curentHookAndRadiusId;
    [SerializeField] private CircleCollider2D _hookCollider2d;
    [SerializeField] private SpriteRenderer _spriteRenderer;

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
        _moveAction.Enable();
        _topEdge = _ship.GetComponent<Collider2D>().bounds.min.y;
        _downEdge = _topEdge - _fishingLineLength;
    }

    void Update()
    {
        _moveVector = _moveAction.ReadValue<Vector2>();
        _downEdge = _topEdge - _fishingLineLength;
        ChangeHook();
    }

    private void FixedUpdate()
    {
        if (!isMove)
        {
            return;
        }

        if (_moveVector != Vector2.zero)
        {
            HookMove();
        }
    }

    private void HookMove()
    {
        var movePositionX = _rigidbody2d.position.x + _moveVector.x * _hookSpeed * Time.deltaTime;
        var movePositionY = _rigidbody2d.position.y + _moveVector.y * _shipSpeed * Time.deltaTime;
        _rigidbody2d.MovePosition(new Vector2(movePositionX, movePositionY));
        Ship.Instance.rigidbody2d.MovePosition(new Vector2(movePositionX, _ship.transform.position.y));

        if (_moveVector.x >= 0)
        {
            _shipModelTransform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            if (_moveVector.x < 0)
            {
                _shipModelTransform.eulerAngles = new Vector3(0, 180f, 0);
            }
        }

        if (_rigidbody2d.position.y < _downEdge)
        {
            _rigidbody2d.position = new Vector2(_rigidbody2d.position.x, _downEdge);
        }
        if (_rigidbody2d.position.y > _topEdge)
        {
            _rigidbody2d.position = new Vector2(_rigidbody2d.position.x, _topEdge);

            if (fishOnHook != null)
            {
                var fish = fishOnHook.GetComponent<Fish>();
                _fishSpawnManager.RemoveFish(fish.gameObject);
                Destroy(fishOnHook);
            }
        }
        _lineRenderer.SetPosition(0, new Vector2(movePositionX, 0));
        _lineRenderer.SetPosition(1, new Vector2(movePositionX, movePositionY));
    }

    private void ChangeHook()
    {
        _spriteRenderer.sprite = _hooksAndRadiusList[_curentHookAndRadiusId].hookSprite;
        _hookCollider2d.radius = _hooksAndRadiusList[_curentHookAndRadiusId].radius;
        _hookCollider2d.offset = _hooksAndRadiusList[_curentHookAndRadiusId].offset;
    }

    /*    private void HookHorizontalMove()
        {
            var positionX = _rigidbody2d.position.x + _moveDirection * _hookSpeed * Time.deltaTime;
            Vector2 movePosition = new Vector2(positionX, transform.position.y);
            _rigidbody2d.MovePosition(movePosition);
        }

        private void HookVerticalMove()
        {

            var positionY = _rigidbody2d.position.y + _moveDirection * _hookSpeed * Time.deltaTime;
            if (fishOnHook != null)
            {
                fishOnHook.transform.position = transform.position;
            }
            
            if (positionY <= _downEdge)
            {
                positionY = _downEdge;
            }

            Vector2 movePosition = new Vector2(_ship.transform.position.x, positionY);
            _rigidbody2d.MovePosition(movePosition);
        }
    */
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
    [System.Serializable]
    struct HooksAndRadius
    {
        public Sprite hookSprite;
        public float radius;
        public Vector2 offset;
    }
}