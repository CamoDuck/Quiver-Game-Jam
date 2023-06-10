using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// CONSTANT ///
    PlayerInput playerInput;
    [SerializeField] Rigidbody2D body;
    const float walkSpeed = 5;

    /// VARYING ///

    void Awake() {
        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    void FixedUpdate() {
        Move();
    }

    void Move() {
        Vector2 movementVector = playerInput.Default.Movement.ReadValue<Vector2>();
        body.velocity = movementVector * walkSpeed;
    }
}
