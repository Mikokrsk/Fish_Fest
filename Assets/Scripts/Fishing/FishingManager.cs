using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FishingManager : MonoBehaviour
{
    [SerializeField] private GameObject _fishingUI;

    private Fish _fishOnHook;
    public static FishingManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void StartFishing(Fish fishOnHook)
    {
        _fishOnHook = fishOnHook;
        _fishOnHook.StopMoving();
        _fishingUI.SetActive(true);
        Hook.Instance.isMove = false;
    }

    public void StopFishing(bool result)
    {
        _fishingUI.SetActive(false);
        Hook.Instance.isMove = true;

        if (result)
        {
            _fishOnHook.CaughtFish();
        }
        else
        {
            FishSpawnManager.Instance.RemoveFish(_fishOnHook.gameObject);
        }
    }
}