using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Canvas UI;
    [SerializeField] TextMeshProUGUI choice1Text;
    [SerializeField] TextMeshProUGUI choice2Text;
    [SerializeField] TextMeshProUGUI choice3Text;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI enemyHealthText;
    [SerializeField] Image enemyPortrait; 
    [SerializeField] GameObject throwableCoworker;
    [SerializeField] Rigidbody2D body;
    [SerializeField] new BoxCollider2D collider;
    [SerializeField] float maxHealth;
    const float playerDamage = 2;

    /// VARYING ///
    List<BaseCoworker> followers = new List<BaseCoworker>();
    BaseCoworker coworker; // current interacting coworker
    DialogChoices currentDialog;

    float currentHealth;

    void Start() {
        currentHealth = maxHealth;
    }

    void updateHealthUI() {
        healthText.text = currentHealth + "/" + maxHealth;
    }

    void updateEnemyHealthUI() {
        float enemyMaxHealth = coworker.getMaxHealth();
        float enemyHealth = coworker.getHealth();
        enemyHealthText.text = enemyHealth + "/" + enemyMaxHealth;
    }
    public void TakeDamage() {
        float value = coworker.attackDamage;

        currentHealth -= value;
        updateHealthUI();
        if (currentHealth < 0) {
            Death();
        } 
    }

    void Death() {
        // send to game over screen
    }

    public void onDialogClick(int choice) {
        StartCoroutine(ThrowCoworkers());
        if (coworker != null) {
            updateEnemyPortrait((Reaction) (choice-1));
            TakeDamage();
            updateChoices();
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Coworker") {
            coworker = other.GetComponent<BaseCoworker>();
            StartInteraction();
        }
    }

    void updateEnemyPortrait(Reaction choice) {
        Sprite sprite = coworker.getPortraitSprite(choice);
        enemyPortrait.sprite = sprite;
    }
    void EndInteraction() {
        coworker.followTarget = followers.Count==0? body: followers[followers.Count-1].body;
        followers.Add(coworker);
        coworker = null;
        UI.transform.gameObject.SetActive(false);
        collider.enabled = true;
    }

    IEnumerator ThrowCoworkers() {
        Vector2 diretion = new Vector2(1,1).normalized;
        float maxTorque = 90;
        float minForceStregth = 4;
        float maxForceStrength = 7;
        float maxThrowDisplacement = 5;
        float maxWaitBetweenThrows = 0.3f;

        List<BaseCoworker> coworkerSynergy = new List<BaseCoworker>();

        // for (int i = 0; i < followers.Count; i++) {
        //     BaseCoworker follower = followers[i];
        //     Reaction reactionType = follower.getReactionType();
        //     if (reactionType == currentDialog.reaction) {
        //         coworkerSynergy.Add(follower);
        //     }
        // }

        coworkerSynergy = followers; // TEMP - this disables the classes system
        int count = coworkerSynergy.Count;
        Debug.Log("run");
        for (int i = 0; i < count; i++) {
            Transform clone = Instantiate(throwableCoworker).transform;
            clone.position = transform.position;
            Rigidbody2D body = clone.GetComponent<Rigidbody2D>();

            BaseCoworker currentFollower = coworkerSynergy[i];
            Sprite followerSprite = currentFollower.sprite;
            clone.GetComponent<SpriteRenderer>().sprite = followerSprite;
            float damage = currentFollower.attackDamage;
            if (coworker != null) {
                AttackCoworker(damage);
            }

            float displaceX = Random.Range(0, maxThrowDisplacement);
            float displaceY = Random.Range(0, maxThrowDisplacement);
            Vector2 throwDisplacement = new Vector2(displaceX, displaceY);
            Vector2 throwPosition = (Vector2)transform.position - throwDisplacement;

            Vector2 force = diretion * Random.Range(minForceStregth,maxForceStrength);
            body.AddForceAtPosition(force, throwPosition, ForceMode2D.Impulse);
            body.angularVelocity = Random.Range(-maxTorque, maxTorque);

            float waitTime = Random.Range(0, maxWaitBetweenThrows);
            yield return new WaitForSeconds(waitTime);
        }
        if (coworker != null) {
            AttackCoworker(playerDamage);
        }

    }


    void AttackCoworker(float damage) {
        bool isDead = coworker.Damage(damage);
        updateEnemyHealthUI();
        if (isDead) {
            EndInteraction();
        }
    }

    void updateChoices() {
        DialogChoices[] currentDialog = coworker.GetInteraction();
        choice1Text.text = currentDialog[0].text;
        choice2Text.text = currentDialog[1].text;
        choice3Text.text = currentDialog[2].text;
    }
    void StartInteraction() {
        collider.enabled = false;
        updateEnemyPortrait(Reaction.Happy);
        updateHealthUI();
        updateEnemyHealthUI();
        updateChoices();
        UI.gameObject.SetActive(true);
    }


}
