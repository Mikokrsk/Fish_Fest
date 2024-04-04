using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    //   [SerializeField] public int direction = 1;
    [SerializeField] public int edgeX = 1;
    [SerializeField] private float _speed = 1;
    [SerializeField] public bool isMoving;
    [SerializeField] private bool isCaucght;
    [SerializeField] public FishAsset fishAsset;

    void Update()
    {
        if (!isMoving)
        {
            return;
        }
        if (transform.position.x <= -edgeX)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            if (transform.position.x >= edgeX)
            {
                transform.eulerAngles = new Vector3(0, 180f, 0);
            }
        }
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    public GameObject CaughtFish()
    {
        if (!isCaucght)
        {
            isMoving = false;
            isCaucght = true;
            transform.Rotate(new Vector3(0, 0, 90f));
            return this.gameObject;
        }
        return null;
    }
}