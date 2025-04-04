using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NPCPathController : MonoBehaviour
{
    public EmptyController emptyController;
    public SteeringController steeringController;

    private void Start()
    {
        steeringController = GetComponent<SteeringController>();
        StartCoroutine(WaitPathSpawned());
    }
    private void HandlePathSpawned(List<GameObject> path)
    {
        steeringController.behaviors.Clear();
        steeringController.behaviors.Add(new PathFollowingBehavior(path, steeringController));
    }
    private IEnumerator WaitPathSpawned()
    {
        yield return new WaitForSeconds(1f);
        HandlePathSpawned(emptyController.path);
    }
}
