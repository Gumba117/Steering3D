using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class AvoidCollisionBehavior : SteeringBehavior
{
    private Vector3 _ahead;

    public float maxSeeAhead, maxAvoidanceForce;
    public List<GameObject> spawnerObjects;
    
    public override Vector3 GetSteeringForce()
    {
        Vector3 avoidance = Vector3.zero;
        _ahead = Velocity.normalized * maxSeeAhead;
        
        Debug.DrawLine(Position, _ahead + Position, Color.green);
        
        GameObject treat = FindMostThreateningObstacle(spawnerObjects);
        if (treat != null)
        {
            avoidance = _ahead - treat.transform.position;
            avoidance = avoidance.normalized * maxAvoidanceForce;
            Debug.DrawLine(Position,treat.transform.position, Color.blue);
            Debug.DrawLine(Position+_ahead,Position+_ahead+avoidance, Color.red);
            
            return avoidance;
        }
        return avoidance;
    }
    public GameObject FindMostThreateningObstacle(List<GameObject> spawnerObjects)
    {
        GameObject biggestTreat = null;
        foreach (GameObject item in spawnerObjects)
        {
            if (ClosestObstacle(item, biggestTreat) && AheadCollision(item, maxSeeAhead))
            {
                biggestTreat = item;
            }
        }
        return biggestTreat;
    }
    private bool AheadCollision(GameObject treat, float obstacleRadius)
    {
        float distance = (treat.transform.position - _ahead).magnitude;
        return distance < obstacleRadius;
    }
    private bool ClosestObstacle(GameObject treat, GameObject biggestTreat)
    {
        if (biggestTreat == null || Vector3.Distance(Position, treat.transform.position)<Vector3.Distance(Position, biggestTreat.transform.position))
        {
            return true;
        }
        return false;
    }
}
