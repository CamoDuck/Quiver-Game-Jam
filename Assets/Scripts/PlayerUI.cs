using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Canvas UI;
    [SerializeField] TextMeshProUGUI choice1Text;
    [SerializeField] TextMeshProUGUI choice2Text;
    [SerializeField] TextMeshProUGUI choice3Text;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI enemyHealthText;

    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject playerPortrait;
    [SerializeField] GameObject enemyPortrait;
    [SerializeField] GameObject choice1Box;
    [SerializeField] GameObject choice2Box;
    [SerializeField] GameObject choice3Box;
    [SerializeField] GameObject healthBox;
    [SerializeField] GameObject enemyHealthBox;
    [SerializeField] GameObject fadeOverlay;

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
        StartCoroutine(MoveToDesiredPosition(dialogueBox, 0.0f));
        StartCoroutine(MoveToDesiredPosition(playerPortrait, 0.1f));
        StartCoroutine(MoveToDesiredPosition(enemyPortrait, 0.15f));
        StartCoroutine(MoveToDesiredPosition(healthBox, 0.1f));
        StartCoroutine(MoveToDesiredPosition(enemyHealthBox, 0.15f));
        StartCoroutine(MoveToDesiredPosition(choice1Box, 0.8f));
        StartCoroutine(MoveToDesiredPosition(choice2Box, 1.0f));
        StartCoroutine(MoveToDesiredPosition(choice3Box, 1.2f));
        fadeOverlay.gameObject.SetActive(true);
        UI.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        StartInteraction();
    }

    IEnumerator MoveToDesiredPosition(GameObject obj, float delay)
    {
        Vector2 desiredPos = new Vector2(obj.GetComponent<RectTransform>().anchoredPosition.x, obj.GetComponent<RectTransform>().anchoredPosition.y);

        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(obj.GetComponent<RectTransform>().anchoredPosition.x, obj.GetComponent<RectTransform>().anchoredPosition.y - 500);
        yield return new WaitForSeconds(delay);
        while (obj.GetComponent<RectTransform>().anchoredPosition.y < (desiredPos.y - 4))
        {
            Vector2 interpolatedPosition = Vector2.Lerp(obj.GetComponent<RectTransform>().anchoredPosition, desiredPos, Time.deltaTime * 7.0f);
            obj.GetComponent<RectTransform>().anchoredPosition = interpolatedPosition;;
            yield return new WaitForEndOfFrame();
        }
        obj.GetComponent<RectTransform>().anchoredPosition = desiredPos;
    }
    

    void StartInteraction() {            
        updateChoices();
    }


}
