using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _fishesList;
    [SerializeField] private float _topEdge;
    [SerializeField] private float _leftEdge;
    [SerializeField] private float _rightEdge;
    [SerializeField] private float _bottomEdge;
    [SerializeField] private float _maxNumFish;
    /*    public static FishSpawnManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }*/

    private void Update()
    {
        if (_fishesList.Count <= _maxNumFish)
        {
            var id = Random.Range(0, _fishesList.Count);
            SpawnFish(_fishesList[id]);
        }
    }

    public void SpawnFish(GameObject fishPref)
    {
        var positionX = Random.Range(-1f, 1f);
        var positionY = Random.Range(_bottomEdge, _topEdge);
        if (positionX < 0)
        {
            positionX = _leftEdge;
        }
        else
        {
            positionX = _rightEdge;
        }
        var position = new Vector2(positionX, positionY);
        var fish = Instantiate(fishPref, transform);
        fish.transform.position = position;
        _fishesList.Add(fish);
    }
    public void CaughtFish(GameObject fish)
    {
        _fishesList.Remove(fish);
    }
}