using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField]private float _enemySpawnTime = 5f, _allySpawnTime = 5f;

    [Header("Normal Enemy Variables")]
    public GameObject enemyPrefab;
    public int initialEnemies;
    public Spawner enemySpawner;

    [Header("Fast Enemy Variables")]
    public GameObject fastEnemyPrefab;
    public int initialFastEnemies;
    public Spawner fastEnemySpawner;

    [Header("MiniBoss Variables")]
    public GameObject miniBossPrefab;
    public int miniBossCount;
    public Spawner miniBossSpawner;

    [Header("Boss Variables")]
    public GameObject bossPrefab;
    public int bossCount;
    public Spawner bossSpawner;

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
    [SerializeField]private CentralTowerController _centralTower;

    public bool speedUp = false;

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
        miniBossSpawner = new Spawner(miniBossPrefab, this);
        bossSpawner = new Spawner(bossPrefab, this);
        enemySpawner.StartTimeSpawning(_enemySpawnTime);
        fastEnemySpawner.StartTimeSpawning(_enemySpawnTime);
        _wanderAllySpawner.StartTimeSpawning(_allySpawnTime);
        _pathAllySpawner.StartTimeSpawning(_allySpawnTime);
        _allySpawner.StartTimeSpawning(_allySpawnTime);
    }
    private void Update()
    {
        if (miniBossCount>=2)
        {
            MiniBossSpawning(_centralTower.lastTowerConquered.gameObject);
        }
        if (bossCount>=5)
        {
            BossSpawning();
        }
        if (speedUp)
        {
            _enemySpawnTime -= (_enemySpawnTime*0.2f);
            StopAllEnemySpawning();
            enemySpawner.StartTimeSpawning(_enemySpawnTime);
            fastEnemySpawner.StartTimeSpawning(_enemySpawnTime);
            speedUp = false;
        }
        if (_centralTower.gameOver)
        {
            StopAllSpawning();
        }
    }
    public void MiniBossSpawning(GameObject tower)
    {
        miniBossPrefab.GetComponent<EnemyController>().centralTower = tower;
        miniBossSpawner.randomSpawn = false;
        miniBossSpawner.spawnPoint = tower.transform.position + Vector3.back;
        miniBossSpawner.StartLimitSpawning(1);
        miniBossCount = 0;
    }
    public void BossSpawning()
    {
        bossPrefab.GetComponent<EnemyController>().centralTower = _centralTower.gameObject;
        bossSpawner.randomSpawn = false;
        bossSpawner.spawnPoint = _centralTower.transform.position + Vector3.back;
        bossSpawner.StartLimitSpawning(1);
        bossCount = 0;
    }
    public void StopAllSpawning()
    {
        enemySpawner.StopSpawning();
        fastEnemySpawner.StopSpawning();
        _wanderAllySpawner.StopSpawning();
        _pathAllySpawner.StopSpawning();
        _allySpawner.StopSpawning();
        miniBossSpawner.StopSpawning();
        bossSpawner.StopSpawning();
    }
    public void StopAllEnemySpawning()
    {
        enemySpawner.StopSpawning();
        fastEnemySpawner.StopSpawning();
    }
}
