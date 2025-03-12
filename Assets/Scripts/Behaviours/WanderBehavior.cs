using UnityEngine;
using UnityEngine.UIElements;
[System.Serializable]
public class WanderBehavior : SteeringBehavior
{
    private Vector3 _wanderTarget;

    public float speed;
    public float slowingRadius = 5;

    public float circleDistance;
    public float circleRadius;

    public float wanderAngle;

    public float targetTime, angleTime;

    private float _targetCoolDown = 0;
    private float _angleCoolDown = 0;

    public override Vector3 GetSteeringForce()
    {
        float desiredSpeed;

        float distance = (_wanderTarget - Position).magnitude;


        if (distance < slowingRadius)
        {
            desiredSpeed = speed * (distance/slowingRadius);
        }
        else
        {
            desiredSpeed = speed;
        }

        //Wander timers
        _angleCoolDown += Time.deltaTime;
        _targetCoolDown += Time.deltaTime;

        _targetCoolDown = Mathf.Clamp(_targetCoolDown, 0, targetTime);
        _angleCoolDown = Mathf.Clamp(_angleCoolDown, 0, angleTime);

        if (_targetCoolDown == targetTime)
        {
            ChangeTarget();
            _targetCoolDown = 0;
        }
        if (_angleCoolDown == angleTime)
        {
            ChangeAngle();
            _angleCoolDown = 0;
        }

        Vector3 steering = (_wanderTarget - Position).normalized * desiredSpeed;

        Vector3 desiredSteering = new Vector3(steering.x, 0, steering.z);

        return desiredSteering + SetAngle() + CirclePos();
    }
    private void ChangeTarget()
    {
        _wanderTarget = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        
    }
    private void ChangeAngle()
    {
        wanderAngle = Random.Range(0, 360);
    }
    private Vector3 CirclePos()
    {
        Vector3 circlepos = (_wanderTarget - Position).normalized * circleDistance;

        Vector3 desiredcirclepos = new Vector3(circlepos.x, 0, circlepos.z);

        return desiredcirclepos;
    }
    private Vector3 SetAngle()
    {
        float angleRad = Mathf.Rad2Deg * wanderAngle;

        Vector3 angle = new Vector3(Mathf.Cos(angleRad) * circleRadius,0,Mathf.Sin(angleRad) * circleRadius);

        return angle / 2;
    }


}
