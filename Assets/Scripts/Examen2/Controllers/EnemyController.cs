using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public enum EnemyType
    {
        Normal,
        Fast,
        MiniBoss,
        Boss
    }
    public SteeringController steeringController;
    public bool onTower;
    public EnemyType enemyType;
    public int health;
    public TowerController[] towers;
    public GameObject centralTower;

    private void Start()
    {
        steeringController = GetComponent<SteeringController>();
        switch (enemyType)
        {
            case EnemyType.Normal:
                health = 1;
                steeringController.behaviors.Clear();
                steeringController.behaviors.Add(new WanderBehavior { speed = 2.5f, circleDistance = 8, circleRadius = 8, angleTime = 1, targetTime = 5 });
                break;
            case EnemyType.Fast:
                health = 1;
                TowerController weakestTower = towers[0];
                foreach (TowerController tower in towers)
                {
                    if (tower.enemiesCount < weakestTower.enemiesCount)
                    {
                        weakestTower = tower;
                    }
                }
                GoToTower(weakestTower.transform);
                break;
            case EnemyType.MiniBoss:
                health = 10;
                steeringController.behaviors.Clear();
                steeringController.behaviors.Add(new SeekBehavior(centralTower.transform, 2) { target = centralTower.transform, speed = 2, slowingRadius = 0 });
                break;
            case EnemyType.Boss:
                health = 20;
                steeringController.behaviors.Clear();
                steeringController.behaviors.Add(new SeekBehavior(centralTower.transform, 2) { target = centralTower.transform, speed = 2, slowingRadius = 0 });
                break;
            default:
                break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ally"))
        {
            if (onTower && (enemyType == EnemyType.Normal || enemyType == EnemyType.Fast))
            {
                
            }
            else
            {
                TakeDamage(1);
            }
        }
    }
    public void GoToTower(Transform tower)
    {
        steeringController.behaviors.Clear();
        steeringController.behaviors.Add(new SeekBehavior(tower, 5) { target = tower, speed = 5, slowingRadius = 0 });
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
