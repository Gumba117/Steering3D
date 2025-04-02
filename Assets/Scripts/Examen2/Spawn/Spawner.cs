using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner
{
    public GameObject prefab;
    public float[] spawnArea = { -70f, 70f, -70f, 70f }, spawnExclude = { -20f, 20f, -20f, 20f };
    public Vector3 spawnPoint;
    public bool randomSpawn = true;
    public List<GameObject> spawnedObjects = new List<GameObject>();

    private MonoBehaviour _controller;
    private Coroutine _spawnRoutine;
    public Spawner(GameObject prefab, MonoBehaviour controller)
    {
        this.prefab = prefab;
        _controller = controller;
    }
    public void StartTimeSpawning(float interval)
    {
        _spawnRoutine = _controller.StartCoroutine(SpawnRoutine(interval));
    }
    public void StopSpawning() 
    {
        _controller.StopCoroutine(_spawnRoutine);
    }
    public void StartLimitSpawning(int limit)
    {
        for (int i = 0; i < limit; i++)
        {
            Spawn();
        }
    }
    private void Spawn()
    {
        Vector3 newPosition;
        if (randomSpawn)
        {
            newPosition = SpawnExclude();
        }
        else
        {
            newPosition = spawnPoint;
        }
        GameObject newObject = GameObject.Instantiate(prefab, newPosition, Quaternion.identity);
        spawnedObjects.Add(newObject);
    }
    private IEnumerator SpawnRoutine(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Spawn();
        }
    }
    private Vector3 SpawnExclude()
    {
        float x, z;
        bool LimitX, LimitZ;
        do
        {
            x = Random.Range(spawnArea[0], spawnArea[1]);
            z = Random.Range(spawnArea[2], spawnArea[3]);

            LimitX = (x > spawnExclude[1] || x < spawnExclude[0]);
            LimitZ = (z > spawnExclude[3] || z < spawnExclude[2]);

        } while (!(LimitX && LimitZ));
        return new Vector3(x, 0.5f, z);
    }
}
