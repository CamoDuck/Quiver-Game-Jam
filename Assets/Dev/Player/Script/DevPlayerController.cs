using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DevPlayerController : MonoBehaviour
{
    DevPlayerInput playerInput;

    public Rigidbody2D movementRigidbody;
    public float movementSpeed;
    public float movementAcceleration;

    public float jumpSpeed;

    void Awake()
    {
        // Input
        playerInput = new DevPlayerInput();
        playerInput.Enable();
        playerInput.Default.Jump.performed += Jump;
    }

    void OnDestroy()
    {
        playerInput.Default.Jump.performed -= Jump;
    }

    void FixedUpdate()
    {
        // Movement
        Vector2 movementVector = playerInput.Default.Movement.ReadValue<Vector2>();
        Vector2 desiredVelocity = new Vector2(movementVector.x * movementSpeed, movementRigidbody.velocity.y);
        movementRigidbody.velocity = Vector2.MoveTowards(movementRigidbody.velocity, desiredVelocity, movementAcceleration * Time.fixedDeltaTime);
    }

    void Jump()
    {
        movementRigidbody.velocity = new Vector2(movementRigidbody.velocity.x, Mathf.Max(jumpSpeed, movementRigidbody.velocity.y));
    }
    void Jump(InputAction.CallbackContext context)
    {
        Jump();
    }
}
