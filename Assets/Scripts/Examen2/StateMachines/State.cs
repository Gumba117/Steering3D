using UnityEngine;

public class State<T>
{ 
    protected T Owner;

    public State(T owner)
    {
        Owner = owner;
    }
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
