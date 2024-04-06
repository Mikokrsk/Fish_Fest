using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private int _edgeX = 13;
    [SerializeField] private float _speed = 1;
    public bool _isMoving = true;
    public bool _isCaught = false;
    [SerializeField] public FishAsset fishAsset;

    private void Start()
    {
        if (transform.position.x >= -_edgeX && transform.position.x <= _edgeX)
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
        if (transform.position.x <= -_edgeX)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }
        else
        {
            if (transform.position.x >= _edgeX)
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