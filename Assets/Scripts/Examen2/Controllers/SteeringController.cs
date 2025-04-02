using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringController : MonoBehaviour
{
    [SerializeReference]
    public List<SteeringBehavior> behaviors = new List<SteeringBehavior>();
    public float maxForce = 10f;
    public Vector3 velocity;

    private void FixedUpdate()
    {
        Vector3 totalForce =  Vector3.zero;
        foreach (var behavior in behaviors)
        {
            behavior.UpdateMovementData(velocity, transform.position);
            totalForce = behavior.GetSteeringForce();
        }

        velocity = Vector3.ClampMagnitude(totalForce, maxForce);
        transform.position += velocity * Time.fixedDeltaTime;
    }
}
