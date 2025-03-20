using System;
using UnityEngine;

public enum EnemyType
{
    Normal,
    Fast,
    Miniboss,
    Boss
}
public class Enemy : Entity
{
    public EnemyType Type { get; private set; }
    //Delegado para manejar las colisiones por tipo de enemy
    public Action OnCollision { get; private set; }
    public SteeringController steeringController;
    public Transform target;

    public Enemy(EnemyType type, SteeringController steeringController, Transform target)
    {
        Type = type;
        this.steeringController = steeringController;
        this.target = target;

        SetAttributes(type);

    }
    private void SetAttributes(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Normal:
                speed = 2f;
                weigth = 1;
                OnCollision = NormalEnemyCollision;
                return;
            case EnemyType.Fast:
                speed = 5f;
                weigth = 1;
                OnCollision = NormalEnemyCollision;
                return;
            case EnemyType.Miniboss:
                speed = 0.5f;
                weigth = 10;
                OnCollision = BosslEnemyCollision;
                return;
            case EnemyType.Boss:
                speed = 0.1f;
                weigth = 20;
                OnCollision = BosslEnemyCollision;
                return;
        }

    }

    public  override void Collision()
    {
        OnCollision?.Invoke();
    }

    private void NormalEnemyCollision()
    {
        //Hacer colision normal
    }
    private void BosslEnemyCollision()
    {
        //Hacer colision normal
    }

}
