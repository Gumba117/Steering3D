using UnityEngine;
using System.Collections.Generic;

public class NPCPathController : MonoBehaviour
{
    public EmptyController emptyController;
    public SteeringController steeringController;

    private void Start()
    {
        steeringController = GetComponent<SteeringController>();

        emptyController.OnpathSpawned += HandlePathSpawned;
    }

    private void HandlePathSpawned(List<GameObject> path)
    {
        steeringController.behaviors.Add(new PathFollowingBehavior(path, steeringController));
        Debug.Log("Path sended");
    }
}
