using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "myAssets/Fish Asset")]
public class FishAsset : ScriptableObject
{
    [Header("Stats")]
    public int id;
    public string fishName;
    public int leftEdge;
    public int rightEdge;
    public int topEdge;
    public int bottomEdge;
    public int speed;
    public int speedUI;

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