using UnityEngine;

public class ObjstacleController : MonoBehaviour
{
    public GameObject prefab;
    public int numObjects;
    public Spawner spawner;
    public float[] spawnArea = { -5f, 5f, -5f, 5f };

    private void Start()
    {
        spawner = new Spawner(prefab, this);
        spawner.spawnArea = spawnArea;
        spawner.StartLimitSpawning(numObjects);
    }
}
