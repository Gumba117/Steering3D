using UnityEngine;

public abstract class Entity 
{
    public Vector3 Position;
    public float speed;
    public int weigth;

    public abstract void Collision();
    
}
