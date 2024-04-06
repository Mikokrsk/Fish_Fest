using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int id;
    public int spawnZone;
    public float leftEdge;
    public float rightEdge;
    [SerializeField] private float _speed;
    public bool isMoving = true;
    public bool isCaught = false;
    [SerializeField] public FishAsset fishAsset;

    private void Awake()
    {
        _speed = fishAsset.speed;
    }

    private void Start()
    {
        if (transform.position.x >= rightEdge && transform.position.x <= leftEdge)
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
        if (isCaught)
        {
            transform.position = Hook.Instance.transform.position;
        }
        if (!isMoving)
        {
            return;
        }
        if (transform.position.x <= leftEdge)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }
        else
        {
            if (transform.position.x >= rightEdge)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }

    public void CaughtFish()
    {
        isMoving = false;
        isCaught = true;
        transform.Rotate(new Vector3(0, 0, -90f));
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    public void ContinuesMoving()
    {
        isMoving = false;
    }
}