using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public static Ship Instance;
    [SerializeField] private int _weight;
    [SerializeField] private int _maxWeight;
    [SerializeField] private Text _weightText;
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
        _weightText.text = $"Weight:\n{_weight}/{_maxWeight}";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hook = collision.gameObject.GetComponent<Hook>();
        if (hook != null)
        {
            if (hook?.fishOnHook != null)
            {
                var fish = hook.fishOnHook.GetComponent<Fish>();
                if (_weight + fish.fishAsset.weight <= _maxWeight)
                {
                    _weight += fish.fishAsset.weight;
                    PlayerInventory.Instance.fishAssetsList.Add(fish.fishAsset);
                    _weightText.text = $"Weight:\n{_weight}/{_maxWeight}";
                }
                FishSpawnManager.Instance.RemoveFish(hook.fishOnHook);
            }
        }
    }
}