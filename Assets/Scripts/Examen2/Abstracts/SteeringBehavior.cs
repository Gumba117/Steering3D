using UnityEngine;
[System.Serializable]
public abstract class SteeringBehavior 
{
    public Vector3 Velocity { get; set; }
    public Vector3 Position { get; set; }
    public abstract Vector3 GetSteeringForce();

    public void UpdateMovementData(Vector3 velocity, Vector3 position) 
    {
        Velocity = velocity;
        Position = position;
    }
}
