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
    [SerializeField] private int _weight;
    [SerializeField] private int _maxWeight;
    [SerializeField] private int _cost;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hook = collision.gameObject.GetComponent<Hook>();
        if (hook != null)
        {
            var fish = hook.fishOnHook.GetComponent<Fish>();
            if (_weight + fish.fishAsset.weight <= _maxWeight)
            {
                _weight += fish.fishAsset.weight;
                _cost += fish.fishAsset.cost;
            }
            FishSpawnManager.Instance.RemoveFish(hook.fishOnHook);
        }
    }
}