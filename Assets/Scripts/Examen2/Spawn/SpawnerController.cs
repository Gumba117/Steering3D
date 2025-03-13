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

    }
    private IEnumerator CreateSeekEnemies(float watingTime)
    {
        yield return new WaitForSeconds(watingTime);

    }
    private IEnumerator CreateFleeEnemies(float watingTime)
    {
        yield return new WaitForSeconds(watingTime);

    }
}
