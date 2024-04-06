using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int id;
    [SerializeField] private int _leftEdge;
    [SerializeField] private int _rightEdge;
    [SerializeField] private float _speed;
    public bool _isMoving = true;
    public bool _isCaught = false;
    [SerializeField] public FishAsset fishAsset;

    private void Awake()
    {
        _leftEdge = fishAsset.leftEdge;
        _rightEdge = fishAsset.rightEdge;
        _speed = fishAsset.speed;
    }

    private void Start()
    {
        if (transform.position.x >= _rightEdge && transform.position.x <= _leftEdge)
        {
            var direction = Random.Range(0, 2);
            if (direction == 0)
            {
                transform.eulerAngles = new Vector3(0, 180f, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    void Update()
    {
        if (!_isMoving)
        {
            return;
        }
        if (transform.position.x <= _leftEdge)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }
        else
        {
            if (transform.position.x >= _rightEdge)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }

    public void CaughtFish()
    {
        _isMoving = false;
        _isCaught = true;
        transform.Rotate(new Vector3(0, 0, -90f));
    }

    public void StopMoving()
    {
        _isMoving = false;
    }

    public void ContinuesMoving()
    {
        _isMoving = false;
    }
}