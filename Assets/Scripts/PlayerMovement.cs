using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private float _horizontalInput;
    private float _verticalInput;



    void Update()
    {

        transform.position += new Vector3(_horizontalInput, 0, _verticalInput) * Time.deltaTime * _speed;
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _horizontalInput = context.ReadValue<Vector2>().x;
        _verticalInput = context.ReadValue<Vector2>().y;


    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _speed = 10f;
        }
        if (context.canceled)
        {
            _speed = 5f;
        }
    }
}
