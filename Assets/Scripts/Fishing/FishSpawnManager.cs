using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishSpawnManager : MonoBehaviour
{
    // [SerializeField] private List<GameObject> _fishesList;
    //  [SerializeField] private List<GameObject> _fishesPrefsList;
    // [SerializeField] private List<FishAsset> _fishAssets;
    //[SerializeField] private List<Collider2D> _spawnZonesList;
    [SerializeField] private List<FishZone> _spawnZonesList;
    /*    [SerializeField] private float _topEdge;
        [SerializeField] private float _leftEdge;
        [SerializeField] private float _rightEdge;
        [SerializeField] private float _bottomEdge;
        [SerializeField] private float _maxNumFish;*/
    public static FishSpawnManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(CheckFishZoneNum());
    }

    public void SpawnFish(int fishZoneId)
    {
        var curentFishZone = _spawnZonesList.Find(x => x.id == fishZoneId);

        var fishPref = curentFishZone.fishesPrefsList[Random.Range(0, curentFishZone.fishesPrefsList.Count)];
        var fish = fishPref.fishPref.GetComponent<Fish>();
        fish.fishAsset = RandomisedFishAsset(fishPref.fishAssets);

        var positionX = Random.Range(curentFishZone.collider.bounds.min.x, curentFishZone.collider.bounds.max.x);
        var positionY = Random.Range(curentFishZone.collider.bounds.min.y, curentFishZone.collider.bounds.max.y);
        var position = new Vector2(positionX, positionY);

        var fishObject = Instantiate(fish, curentFishZone.collider.gameObject.transform);
        fish.transform.position = position;
        fish.leftEdge = curentFishZone.collider.bounds.min.x;
        fish.rightEdge = curentFishZone.collider.bounds.max.x;

        curentFishZone.fishGameObjects.Add(fishObject.gameObject);
    }

    private FishAsset RandomisedFishAsset(List<FishAsset> fishAssets)
    {
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
        for (int i = 0; i < _spawnZonesList.Count; i++)
        {
            if (_spawnZonesList[i].fishGameObjects.Find(x => x.gameObject == fish))
            {
                Destroy(fish);
                SpawnFish(i);
            }
        }
    }

    IEnumerator CheckFishZoneNum()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _spawnZonesList.Count;)
        {
            if (_spawnZonesList[i].fishGameObjects.Count < _spawnZonesList[i].num)
            {
                SpawnFish(i);
            }
            else
            {
                i++;
            }
        }

        //   StartCoroutine(CheckFishZoneNum());
    }

    [System.Serializable]
    public struct FishZone
    {
        public int id;
        public Collider2D collider;
        public int num;
        public List<FishPref> fishesPrefsList;
        public List<GameObject> fishGameObjects;
    }
    [System.Serializable]
    public struct FishPref
    {
        public GameObject fishPref;
        public List<FishAsset> fishAssets;
    }
}