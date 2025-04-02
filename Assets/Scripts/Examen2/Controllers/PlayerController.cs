using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    public Rigidbody rb;
    public float moveSpeed;

    [SerializeField] private  Material _material;
    [SerializeField] private GameObject[] _towers;
    private bool _isStunned = false;
    private List<AllyController> _allysFollowing;

    //private SteeringController _steeringController; //AVOID COLLISION BEHAVIOR
    //[SerializeField] private ObjstacleController _obstacleController; //AVOID COLLISION BEHAVIOR
    //[SerializeField] private SpawnerController _spawnerController; //AVOID COLLISION BEHAVIOR
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        /*
         * Avoid Collision Behavior
         *         Que se supone que deberia funcionar
         *                 Pero me dijo que siguiera con el examen... 
        _steeringController.behaviors.Clear();
        _steeringController = GetComponent<SteeringController>();
        if (_obstacleController == null)
        {
            _steeringController.behaviors.Add(new AvoidCollisionBehavior() { spawnerObjects = _spawnerController.enemySpawner.spawnedObjects, maxSeeAhead = 1f,  maxAvoidanceForce = 1f });
        }
        else
        {
            _steeringController.behaviors.Add(new AvoidCollisionBehavior() { spawnerObjects = _obstacleController.spawner.spawnedObjects, maxSeeAhead = 1f,  maxAvoidanceForce = 1f });
        }
        */
    }
    void FixedUpdate()
    {
        MovePlayer();
        SelectTower();
    }
    private void MovePlayer()
    {
        if (_isStunned) return;
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = cameraTransform.forward * moveZ + cameraTransform.right * moveX;
        moveDirection.y=0f;

        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Acceleration);
        //_steeringController.velocity = rb.linearVelocity; //AVOID COLLISION BEHAVIOR
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Stun(3f));
        }
    }
    private IEnumerator Stun(float watingTime)
    {
        _isStunned = true;
        _material.color = Color.yellow;
        yield return new WaitForSeconds(watingTime);
        _isStunned = false;
        _material.color = Color.blue;

    }
    public void AddAlly(GameObject ally)
    {
        if (_allysFollowing == null)
        {
            _allysFollowing = new List<AllyController>();
        }
        _allysFollowing.Add(ally.GetComponent<AllyController>());
    }
    public void SelectTower()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AllysToTower(_towers[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AllysToTower(_towers[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AllysToTower(_towers[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AllysToTower(_towers[3]);
        }
    }
    public void RemoveAllAllys()
    {
        if (_allysFollowing == null) return;
        _allysFollowing.Clear();
    }
    public void AllysToTower(GameObject tower)
    {
        if (_allysFollowing == null) return;
        foreach (AllyController ally in _allysFollowing)
        {
            ally.GoToTower(tower.transform);
        }
        RemoveAllAllys();
    }
}
