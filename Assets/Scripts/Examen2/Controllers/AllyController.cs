using UnityEngine;

public class AllyController : MonoBehaviour
{
    public enum AllyType
    {
        Wander,
        PathFollow,
        Estatic
    }
    public SteeringController steeringController;
    public bool onTower;
    public AllyType allyType;

    private void Start()
    {
        steeringController = GetComponent<SteeringController>();
        switch (allyType)
        {
            case AllyType.Wander:
                steeringController.behaviors.Clear();
                steeringController.behaviors.Add(new WanderBehavior { speed = 2.5f, circleDistance = 8, circleRadius = 8, angleTime = 1, targetTime = 5 });
                break;
            case AllyType.PathFollow:
                break;
            case AllyType.Estatic:
                steeringController.behaviors.Clear();
                break;
        }
    }
    public void GoToTower(Transform tower)
    {
        steeringController.behaviors.Clear();
        steeringController.behaviors.Add(new SeekBehavior(tower, 5) { target = tower, speed = 5, slowingRadius = 0 });
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && onTower == false)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            GoToPlayer(collision.transform);
            collision.gameObject.GetComponent<PlayerController>().AddAlly(this.gameObject);
        }
    }
    //HACER UN FOLLOW THE LIDER
    public void GoToPlayer(Transform player)
    {
        steeringController.behaviors.Clear();
        steeringController.behaviors.Add(new SeekBehavior(player, 5) { target = player, speed = 10, slowingRadius = 1 });
    }
}
