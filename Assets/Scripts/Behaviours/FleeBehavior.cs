using UnityEngine;
[System.Serializable]
public class FleeBehavior : SteeringBehavior
{
    public Transform target;
    public float speed;
    public float slowingRadius = 5;

    public override Vector3 GetSteeringForce()
    {

        float desiredSpeed;

        float distance = (target.position - Position).magnitude;

        if (distance < slowingRadius)
        {
            desiredSpeed = speed * (slowingRadius / distance);
        }
        else
        {
            desiredSpeed = speed;
        }

        return -(target.position - Position).normalized * desiredSpeed;
       
    }
}
