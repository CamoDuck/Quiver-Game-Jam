using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCoworker : MonoBehaviour
{
    public Sprite[] portrait;
    
    public string coworkerName;

    public Rigidbody2D movementRigidbody;

    public Rigidbody2D followTarget;
    public float followSpeed;
    public float followDistance;

    void FixedUpdate()
    {
        if ((followTarget.position - movementRigidbody.position).sqrMagnitude > followDistance) {
            movementRigidbody.MovePosition(Vector2.MoveTowards(movementRigidbody.position, followTarget.position, followSpeed * Time.fixedDeltaTime));
        }
    }
    
    public string[] GetInteraction(int index)
    {
        // Return the dialog options for the first dialog interaction
        // Always return three options
        string[] defaultDialog = {"dialog 1", "dialog 2", "dialog 3"};
        return defaultDialog;
    }
    
    public bool TryInteraction(int index, int dialogOption)
    {
        // Return whether the given dialog option for the given dialog prompt was successful
        return false;
    }
}
