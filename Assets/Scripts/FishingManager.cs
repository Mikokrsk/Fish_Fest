using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FishingManager : MonoBehaviour
{
    [SerializeField] private GameObject _fishingUI;

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

    public void StartFishing()
    {
        _fishingUI.SetActive(true);
    }
    public void StopFishing(bool result)
    {
        _fishingUI.SetActive(false);
        if (result)
        {
            Debug.Log("Fish " + result);
        }
        else
        {
            Debug.Log("Fish " + result);
        }
    }
}