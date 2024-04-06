using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Ship : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public static Ship Instance;

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

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}
