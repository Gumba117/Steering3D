using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class PathFollowingBehavior : SteeringBehavior
{
    public List<GameObject> path;
    public float pointRadius = 5f;
    public float speed = 10f;
    public bool looping = true;
    public event Action HandlePathDirection;

    private SeekBehavior _seek;
    private int _currentNode = 0;
    private int _pathDirection= 1;

    public PathFollowingBehavior(List<GameObject> path, SteeringController steering)
    {
        this.path = path;
        _seek = steering.behaviors.OfType<SeekBehavior>().FirstOrDefault();
        if (_seek == null)
        {
            _seek = new SeekBehavior(path[_currentNode].transform, speed);
            steering.behaviors.Add(_seek);
        }
        _seek.speed = speed;
        _seek.target = path[_currentNode].transform;
    }
    public override Vector3 GetSteeringForce()
    {
        if (IsAtNode())
        {
            HandlePathDirection?.Invoke();
            UpdateCurrentNode();
        }
        
        return _seek.GetSteeringForce();
    }
    private bool IsAtNode()
    {
        return (Position - path[_currentNode].transform.position).magnitude < pointRadius;
    }
    private void UpdateCurrentNode()
    {
        _currentNode += _pathDirection;

        HandlePathDirection = looping ? HandleLooping : HandleNonLooping;

        _seek.target = path[_currentNode].transform;
    }
    private void HandleLooping()
    {
        if (_currentNode >= path.Count - 1 || _currentNode <= 0)
        {
            _pathDirection *= -1;
            _currentNode += _pathDirection;
        }
    }
    private void HandleNonLooping()
    {
        if (_currentNode < 0)
        {
            _currentNode = 0;
        }
        if (_currentNode >= path.Count-1)
        {
            _currentNode -= 1;
        }
    }
}
