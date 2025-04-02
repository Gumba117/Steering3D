using System.Collections;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [Header("Normal Enemy Variables")]
    public GameObject enemyPrefab;
    public int initialEnemies;
    public Spawner enemySpawner;

    [Header("Fast Enemy Variables")]
    public GameObject fastEnemyPrefab;
    public int initialFastEnemies;
    public Spawner fastEnemySpawner;

    [Header("Wander Ally Variables")]
    public GameObject wanderAllyPrefab;
    public int initialWanderAllys;
    private Spawner _wanderAllySpawner;

    [Header("Path Ally Variables")]
    public GameObject pathAllyPrefab;
    public int initialPathAllys;
    private Spawner _pathAllySpawner;

    [Header("Ally Variables")]
    public GameObject allyPrefab;
    public int initialAllys;
    private Spawner _allySpawner;

    [SerializeField]private TowerController[] _towers;

    //INYECTAR TOWERS A LOS ENEMIGOS RAPIDOS

    public void Start()
    {
        enemySpawner = new Spawner(enemyPrefab, this);
        enemySpawner.StartLimitSpawning(initialEnemies);

        fastEnemyPrefab.GetComponent<EnemyController>().towers = _towers;
        fastEnemySpawner = new Spawner(fastEnemyPrefab, this);
        fastEnemySpawner.StartLimitSpawning(initialFastEnemies);

        _wanderAllySpawner = new Spawner(wanderAllyPrefab, this);
        _wanderAllySpawner.StartLimitSpawning(initialWanderAllys);

        _pathAllySpawner = new Spawner(pathAllyPrefab, this);
        _pathAllySpawner.StartLimitSpawning(initialPathAllys);

        _allySpawner = new Spawner(allyPrefab, this);
        _allySpawner.StartLimitSpawning(initialAllys);

        
    }

    
}
