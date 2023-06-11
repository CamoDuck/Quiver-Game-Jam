using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// CONSTANT ///
    PlayerInput playerInput;
    [SerializeField] Rigidbody2D body;
    public float walkSpeed;
    public bool playerCanMove;
    [HideInInspector]
    public Vector2 moveDir; 

    /// VARYING ///

    void start() {
        body = GetComponent<Rigidbody2D>();
        

    }

    void Update(){
        InputManagement();     
    }

    void Awake() {
        playerCanMove = true;
        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    void InputManagement() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized; 
    }

    void FixedUpdate(){
        if(playerCanMove == true)
        {
            Move();
        }
        else
        {
            body.velocity = new Vector2(0, 0);
        }
    }

    void Move() {
        body.velocity = new Vector2 (moveDir.x * walkSpeed, moveDir.y * walkSpeed);
    }
}
