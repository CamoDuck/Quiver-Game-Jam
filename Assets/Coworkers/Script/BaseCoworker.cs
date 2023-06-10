using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BaseCoworker : MonoBehaviour
{
    /// CONSTANT ///
    [SerializeField] Rigidbody2D body;
    [SerializeField] Rigidbody2D followTarget;
    [SerializeField] float followSpeed;
    [SerializeField] float followDistance;
    [SerializeField] protected Sprite[] portrait;
    [SerializeField] string coworkerName;
    [SerializeField] float maxHealth;


    /// VARYING ///
    float currentHealth;

    protected DialogChoices dialog = 
    new DialogChoices("Start",
        new DialogChoices("dialog 1"), 
        new DialogChoices("dialog 2"), 
        new DialogChoices("dialog 3")
    );

    void Start() {
        currentHealth = maxHealth;
    }

    void FixedUpdate() {
        if (followTarget == null) {return;}
        float distFromTarget = (followTarget.position - body.position).magnitude;
        if (distFromTarget > followDistance) {
            body.MovePosition(Vector2.MoveTowards(body.position, followTarget.position, followSpeed * Time.fixedDeltaTime));
        }
    }
    
    /// damage this coworker
    public void Damage(float value) {

        if (currentHealth <= 0) {
            Death();
        }
    }

    // called when coworker is defeated in verbal battle
    void Death() {

    }

    /// Return the dialog options for the first dialog interaction, Always return three options
    public string[] GetInteraction() {
        string[] dialogText = {dialog.nextFirst.text, dialog.nextSecond.text, dialog.nextThird.text};

        return dialogText;
    }
    
    /// Return whether the given dialog option for the given dialog prompt was successful
    public bool TryInteraction(int dialogOption) {
        // move down the dialogue tree
        if (dialogOption == 1) {
            dialog = dialog.nextFirst;
        }
        else if (dialogOption == 2) {
            dialog = dialog.nextSecond;      
        }
        else if (dialogOption == 3){
            dialog = dialog.nextThird;
        }
        else {
            Debug.LogError("Invalid dialog option");
        }

        return false;
    }
}
