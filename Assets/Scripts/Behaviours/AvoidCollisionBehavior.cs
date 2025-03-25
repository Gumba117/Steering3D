using UnityEngine;
using System.Collections.Generic;

[System.Serializable]

public class AvoidCollisionBehavior : SteeringBehavior
{
    public Vector3 ahead;

    public float maxSeeAhead;

    public float maxAvoidanceForce;

    public List<GameObject> spawnerObjects;
    
    public override Vector3 GetSteeringForce()
    {

        ahead = Velocity.normalized * maxSeeAhead;
        GameObject mostThreateningObject = FindMostThreateningObstacle(spawnerObjects);

        if (mostThreateningObject!=null)
        {
            Vector3 avoidance = ahead - mostThreateningObject.transform.position;
            avoidance = avoidance.normalized * maxAvoidanceForce;
            Debug.DrawLine(mostThreateningObject.transform.position, avoidance, Color.red);
            Debug.DrawLine(Position, ahead, Color.green);
            return avoidance;
        }

        return ahead;


    }
    public GameObject FindMostThreateningObstacle(List<GameObject> spawnerObjects)
    {
        float maxDistance = Mathf.Infinity;

        //No funciona
        //No cambia al enemigo mas cercano

        foreach (GameObject obstacle in spawnerObjects)
        {
            if ((obstacle.transform.position - Position).magnitude < maxDistance)
            {
                maxDistance = (obstacle.transform.position - Position).magnitude;

                return obstacle;
            }
            else return null;
        }
        return null;
    }


}
