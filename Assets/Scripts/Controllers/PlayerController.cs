using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    public Rigidbody rb;
    public float moveSpeed;

    private SteeringController _steeringController;

    [SerializeField] private ObjstacleController _obstacleController;
    [SerializeField] private SpawnerController _spawnerController;

    private void Start()
    {
        _steeringController = GetComponent<SteeringController>();
        _steeringController.behaviors.Clear();

        rb = GetComponent<Rigidbody>();

        if (_obstacleController == null)
        {
            _steeringController.behaviors.Add(new AvoidCollisionBehavior() { spawnerObjects = _spawnerController.enemySpawner.spawnedObjects, maxSeeAhead = 1f,  maxAvoidanceForce = 1f });
        }
        else
        {
            _steeringController.behaviors.Add(new AvoidCollisionBehavior() { spawnerObjects = _obstacleController.spawner.spawnedObjects, maxSeeAhead = 1f,  maxAvoidanceForce = 1f });
        }

        
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = cameraTransform.forward * moveZ + cameraTransform.right * moveX;
        moveDirection.y=0f;

        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Acceleration);
        _steeringController.velocity = rb.linearVelocity;
    }

}
