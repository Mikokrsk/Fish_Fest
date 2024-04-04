using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "myAssets/Fish Asset")]
public class FishAsset : ScriptableObject
{
    public string fishName;
    public int weight;
    public int cost;
    public Rarity rarity;

}
public enum Rarity
{
    Common,
    Rare,
    Legendary
}