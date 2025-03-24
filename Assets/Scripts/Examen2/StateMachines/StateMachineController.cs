using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class StateMachineController : MonoBehaviour
{
    Entity entity;

    StateMachine<Enemy> stateMachine;

    State<Enemy> state;

    SteeringController steeringController;

    Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine = new StateMachine<Enemy>();
        //entity = new Enemy( EnemyType.Normal, steeringController, player, stateMachine,);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
