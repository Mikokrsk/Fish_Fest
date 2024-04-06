using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _fishesList;
    [SerializeField] private List<GameObject> _fishesPrefsList;
    [SerializeField] private List<FishAsset> _fishAssets;
    [SerializeField] private List<Collider2D> _spawnZonesList;
    [SerializeField] private float _topEdge;
    [SerializeField] private float _leftEdge;
    [SerializeField] private float _rightEdge;
    [SerializeField] private float _bottomEdge;
    [SerializeField] private float _maxNumFish;
    public static FishSpawnManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            /*                DontDestroyOnLoad(gameObject);*/
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_fishesList.Count < _maxNumFish)
        {
            var id = Random.Range(0, _fishesPrefsList.Count);
            SpawnFish(_fishesPrefsList[id]);
        }
    }

    public void SpawnFish(GameObject fishPref)
    {
        var fish = fishPref.GetComponent<Fish>();
        fish.fishAsset = RandomisedFishAsset(fish.id);

        /*        var positionX = Random.Range(fish.fishAsset.leftEdge, fish.fishAsset.rightEdge);
                var positionY = Random.Range(fish.fishAsset.topEdge, fish.fishAsset.bottomEdge);*/
        var curentSpawnZone = _spawnZonesList[fish.spawnZone];

        var positionX = Random.Range(curentSpawnZone.bounds.min.x, curentSpawnZone.bounds.max.x);
        var positionY = Random.Range(curentSpawnZone.bounds.min.y, curentSpawnZone.bounds.max.y);

        var position = new Vector2(positionX, positionY);

        var fishObject = Instantiate(fishPref, _spawnZonesList[fish.spawnZone].gameObject.transform);
        fish.transform.position = position;
        fish.leftEdge = curentSpawnZone.bounds.min.x;
        fish.rightEdge = curentSpawnZone.bounds.max.x;
        _fishesList.Add(fishObject);
    }

    private FishAsset RandomisedFishAsset(int fishId)
    {
        var fishAssets = _fishAssets.FindAll(x => x.id == fishId);
        // Common = 60% [4-9] Rare = 30% [1-3] Legendary = 10% [0]
        var rarity = Random.Range(0, 10);

        if (rarity == 0)
        {
            return fishAssets.Find(x => x.rarity == Rarity.Legendary);
        }

        if (rarity >= 1 && rarity <= 3)
        {
            return fishAssets.Find(x => x.rarity == Rarity.Rare);
        }
        return fishAssets.Find(x => x.rarity == Rarity.Common);
    }

    public void RemoveFish(GameObject fish)
    {
        _fishesList.Remove(fish);
        Destroy(fish);
    }
}