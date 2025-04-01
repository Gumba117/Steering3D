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
        //emptyController.OnpathSpawned += HandlePathSpawned;

    }

    private void HandlePathSpawned(List<GameObject> path)
    {
        steeringController.behaviors.Add(new PathFollowingBehavior(path, steeringController));
        //Debug.Log("Path sended");
    }

    private IEnumerator WaitPathSpawned()
    {
        yield return new WaitForSeconds(1f);
        HandlePathSpawned(emptyController.path);
    }
}
