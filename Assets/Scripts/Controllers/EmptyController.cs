using UnityEngine;
using System;
using System.Collections.Generic;

public class EmptyController : MonoBehaviour
{
    public GameObject prefab;
    public int obstacleLimit = 10;

    public event Action <List<GameObject>> OnpathSpawned;
    public List<GameObject> path;

    private Spawner _emptySpawner;
    private void Start()
    {
        _emptySpawner = new Spawner(prefab, this);
        _emptySpawner.StartLimitSpawning(obstacleLimit);

        path = _emptySpawner.spawnedObjects;
        OnpathSpawned?.Invoke(path);
    }
}
