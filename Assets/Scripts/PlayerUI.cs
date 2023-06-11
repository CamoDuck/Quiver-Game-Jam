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

    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject choice1Box;
    [SerializeField] GameObject choice2Box;
    [SerializeField] GameObject choice3Box;
    [SerializeField] GameObject healthBox;
    [SerializeField] GameObject enemyHealthBox;

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
            TakeDamage();
            updateChoices();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Coworker") {
            coworker = other.GetComponent<BaseCoworker>();
            StartCoroutine(InteractionTransition(1));
        }
    }

    void EndInteraction() {
        coworker.followTarget = followers.Count == 0 ? body : followers[followers.Count - 1].body;
        followers.Add(coworker);
        coworker = null;
        UI.transform.gameObject.SetActive(false);
        collider.enabled = true;
    }

    IEnumerator ThrowCoworkers() {
        Vector2 diretion = new Vector2(1, 1).normalized;
        float minForceStregth = 5;
        float maxForceStrength = 10;
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
        for (int i = 0; i < count; i++) {
            Transform clone = Instantiate(throwableCoworker).transform;
            Rigidbody2D body = clone.GetComponent<Rigidbody2D>();

            BaseCoworker currentFollower = coworkerSynergy[i];
            Sprite followerSprite = currentFollower.sprite;
            clone.GetComponent<SpriteRenderer>().sprite = followerSprite;
            float damage = currentFollower.attackDamage;
            AttackCoworker(damage);
            if (coworker == null) { yield break; }

            float displaceX = Random.Range(0, maxThrowDisplacement);
            float displaceY = Random.Range(0, maxThrowDisplacement);
            Vector2 throwDisplacement = new Vector2(displaceX, displaceY);
            Vector2 throwPosition = (Vector2)transform.position - throwDisplacement;

            Vector2 force = diretion * Random.Range(minForceStregth, maxForceStrength);
            body.AddForceAtPosition(force, throwPosition, ForceMode2D.Impulse);

            float waitTime = Random.Range(0, maxWaitBetweenThrows);
            yield return new WaitForSeconds(waitTime);
        }
        AttackCoworker(playerDamage);

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

    IEnumerator InteractionTransition(int reverse)
    {
        collider.enabled = false;
        updateHealthUI();
        updateEnemyHealthUI();
        UI.gameObject.SetActive(true);
        //int i = 0;

        //Vector3 desiredPosition = new Vector3(dialogueBox.transform.position.x, dialogueBox.transform.position.y + (70 * reverse), 0.0f);
        Vector3 desiredPosition = dialogueBox.transform.position;
        dialogueBox.transform.position = new Vector3(dialogueBox.transform.position.x, dialogueBox.transform.position.y - (dialogueBox.transform.position.y * 2), 0.0f);
        while (dialogueBox.transform.position.y < (desiredPosition.y - 5))
        {
            Vector3 currentPosition = dialogueBox.transform.position;
            Vector3 interpolatedPosition = Vector3.Lerp(currentPosition, desiredPosition, Time.deltaTime * 5.0f);
            dialogueBox.transform.position = interpolatedPosition;

            //dialogueBox.transform.position = new Vector3(dialogueBox.transform.position.x, dialogueBox.transform.position.y + (10 * Time.deltaTime), 0.0f);
            yield return new WaitForEndOfFrame();
        }
        dialogueBox.transform.position = desiredPosition;
        StartInteraction();
    }


    

    void StartInteraction() {            
        updateChoices();
    }


}
