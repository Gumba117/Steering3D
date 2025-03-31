using UnityEngine;

public class EscapeEnemyController : MonoBehaviour
{
    [SerializeField] Transform _player;
    public float distanceMax;

    private SteeringController _controller;


    void Start()
    {
        _controller = GetComponent<SteeringController>();

    }


    void Update()
    {
        float disatance = Vector3.Distance(transform.position, _player.position);

        if (disatance>distanceMax)
        {
            _controller.behaviors.Clear();
            
            _controller.behaviors.Add(new WanderBehavior {speed = 2.5f, circleDistance = 8, circleRadius=8, angleTime = 1, targetTime=1});
        }
        else
        {
            _controller.behaviors.Clear();
            _controller.behaviors.Add(new FleeBehavior { target = _player, speed = 2.5f});
        }
    }
}
