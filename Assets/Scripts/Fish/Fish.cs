using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    //   [SerializeField] public int direction = 1;
    [SerializeField] private int _edgeX = 1;
    [SerializeField] private float _speed = 1;
    private bool _isMoving;
    private bool _isCaucght;
    [SerializeField] public FishAsset fishAsset;

    private void Start()
    {
        if (transform.position.x >= -_edgeX && transform.position.x <= _edgeX)
        {
            var direction = Random.Range(0, 2);
            if (direction == 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180f, 0);
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
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            if (transform.position.x >= _edgeX)
            {
                transform.eulerAngles = new Vector3(0, 180f, 0);
            }
        }
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    public GameObject CaughtFish()
    {
        if (!_isCaucght)
        {
            _isMoving = false;
            _isCaucght = true;
            transform.Rotate(new Vector3(0, 0, 90f));
            return this.gameObject;
        }
        return null;
    }
}