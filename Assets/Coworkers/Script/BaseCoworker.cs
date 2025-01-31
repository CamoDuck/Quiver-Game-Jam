using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BaseCoworker : MonoBehaviour
{
    /// CONSTANT ///
    [SerializeField] public Rigidbody2D body;
    [SerializeField] public Rigidbody2D followTarget;
    [SerializeField] float followSpeed;
    [SerializeField] float followDistance;
    [SerializeField] protected Sprite[] portrait;
    [SerializeField] string coworkerName;
    [SerializeField] float maxHealth;
    public Sprite sprite;
    [SerializeField] public float attackDamage;
    [SerializeField] protected Reaction reactionType;


    /// VARYING ///
    float currentHealth;

    protected DialogChoices[] joke;
    protected DialogChoices[] happy;
    protected DialogChoices[] sad;

    public DialogChoices[] jokeR;
    public DialogChoices[] happyR;
    public DialogChoices[] sadR;

    //ANIMATIONS//
    //References 
    Animator am;
    //PlayerMovement pm;
    SpriteRenderer sr;

    void Start() {
        currentHealth = maxHealth;
        am = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    void FixedUpdate() {
        if (followTarget == null) {return;}

        float distFromTarget = (followTarget.position - body.position).magnitude;
        if (distFromTarget > followDistance) {
            am.SetBool("Move", true);
            body.MovePosition(Vector2.MoveTowards(body.position, followTarget.position, followSpeed * Time.fixedDeltaTime));
        }
    }

    void Update()
    {
        if (body.velocity.x != 0 || body.velocity.y != 0)
        {
            Debug.Log("moving!");
            am.SetBool("Move", true);
            SpriteDirectionChecker();
        }
        else
        {
            // am.SetBool("Move", false);
        }
    }

    public void setReactionType(Reaction reactionType) {
        this.reactionType = reactionType;
    }
    public float getMaxHealth() {
        return maxHealth;
    }
    public float getHealth() {
        return currentHealth;
    }

    public Sprite getPortraitSprite(Reaction reaction) {
        return portrait[(int)reaction];
    }

    public Reaction getReactionType() {
        return reactionType;
    }

    /// damage this coworker, return true if coworker is defeated
    public bool Damage(float value) {
        currentHealth -= value;
        if (currentHealth <= 0) {
            Death();
            return true;
        }
        return false;
    }

    // called when coworker is defeated in verbal battle
    protected virtual void Death() {
        gameObject.tag = "Follower";
    }

    /// Return the dialog options for the first dialog interaction, Always return three options
    public DialogChoices[] GetInteraction() {
        DialogChoices randomJoke = joke[Random.Range(0, joke.Length)];
        DialogChoices randomHappy = happy[Random.Range(0, happy.Length)];
        DialogChoices randomSad = sad[Random.Range(0, sad.Length)];
        DialogChoices[] dialog = {randomJoke, randomHappy, randomSad};

        return dialog;
    }
    void SpriteDirectionChecker()
    {
        if (body.velocity.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    /// Return whether the given dialog option for the given dialog prompt was successful
    // public bool TryInteraction(int dialogOption) {
    //     // move down the dialogue tree
    //     if (dialogOption == 1) {
    //         dialog = dialog.nextFirst;
    //     }
    //     else if (dialogOption == 2) {
    //         dialog = dialog.nextSecond;      
    //     }
    //     else if (dialogOption == 3){
    //         dialog = dialog.nextThird;
    //     }
    //     else {
    //         Debug.LogError("Invalid dialog option");
    //     }

    //     return false;
    // }
}
