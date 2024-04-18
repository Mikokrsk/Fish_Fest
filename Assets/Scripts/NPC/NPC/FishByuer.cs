using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishByuer : MonoBehaviour
{
    public void SellFish()
    {
        var sum = 0;
        foreach (var fish in PlayerInventory.Instance.fishAssetsList)
        {
            sum += fish.cost;
        }
        Debug.Log($"Sum = {sum}");
        PlayerInventory.Instance.fishAssetsList.Clear();
    }
}
