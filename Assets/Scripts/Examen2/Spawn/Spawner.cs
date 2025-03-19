using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner
{
    public GameObject prefab;
    public float[] spawnArea = { -40f, 40f, -40f, 40f };
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
        Vector3 newPosition = new Vector3(Random.Range(spawnArea[0], spawnArea[1]), 0.5f, Random.Range(spawnArea[2], spawnArea[3]));
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
}
