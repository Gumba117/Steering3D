using UnityEngine;
//Atack
public class Atack : State<Enemy>
{
    //Enemy va a tener Target y speed
    //Donde obtengo el objeto enemy => Enemy
    public Atack(Enemy owner) : base(owner)
    {
        SeekBehavior seek = new SeekBehavior(Owner.target, Owner.speed);
        owner.steeringController.behaviors.Clear();
        owner.steeringController.behaviors.Add(seek);
    }
}
//Defend
public class Defend : State<Enemy>
{
    public Defend(Enemy owner) : base(owner)
    {
        Debug.Log("Entrando en Defensa");
    }
}
//Conquer
public class Conquer : State<Enemy>
{
    public Conquer(Enemy owner) : base(owner)
    {
        Debug.Log("Entrando en Conquitador");
    }
}