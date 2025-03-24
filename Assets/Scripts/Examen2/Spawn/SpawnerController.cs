using System.Collections;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [Header("Enemy Variables")]
    public GameObject enemyPrefab;
    public int initialEnemies;
    private Spawner _enemySpawner;

    [Header("NPC Variables")]
    public GameObject npcPrefab;
    public int initialNpcs;
    private Spawner _npcSpawner;

    public void Start()
    {
        _enemySpawner = new Spawner(enemyPrefab, this);
        _enemySpawner.StartLimitSpawning(initialEnemies);

        _npcSpawner = new Spawner(npcPrefab, this);
        _npcSpawner.StartLimitSpawning(initialNpcs);


        StartCoroutine(CreateSeekEnemies(5));
        StartCoroutine(CreateFleeEnemies(5));

    }
    private IEnumerator CreateSeekEnemies(float watingTime)
    {
        yield return new WaitForSeconds(watingTime);
        foreach (GameObject enemy in _enemySpawner.spawnedObjects)
        {
            enemy.GetComponent<SteeringController>().behaviors.Clear();
            
            enemy.GetComponent<SteeringController>().behaviors.Add(new SeekBehavior(  GameObject.Find("Player").transform, 5) { target = GameObject.Find("Player").transform, speed = 5, slowingRadius = 5 });
        }
    }

    private IEnumerator CreateFleeEnemies(float watingTime)
    {
        yield return new WaitForSeconds(watingTime);
        foreach (GameObject enemy in _npcSpawner.spawnedObjects)
        {
            enemy.GetComponent<SteeringController>().behaviors.Clear();
            enemy.GetComponent<SteeringController>().behaviors.Add(new FleeBehavior { target = GameObject.Find("Player").transform, speed = 5, slowingRadius = 5 });
        }
    }
}
