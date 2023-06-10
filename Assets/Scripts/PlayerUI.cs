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
    [SerializeField] GameObject throwableCoworker;
    [SerializeField] Rigidbody2D body;

    /// VARYING ///
    List<BaseCoworker> followers = new List<BaseCoworker>();
    BaseCoworker coworker; // current interacting coworker
    DialogChoices currentDialog;

    public void onDialogClick(int choice) {
        StartCoroutine(ThrowCoworkers());
        bool isDead = coworker.TryInteraction(choice);
        if (isDead) {
            AddCoworker();
        }
        updateChoices();
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Coworker") {
            coworker = other.GetComponent<BaseCoworker>();
            StartInteraction();
        }
    }

    void AddCoworker() {
        followers.Add(coworker);
        coworker.followTarget = body;
        coworker = null;
    }

    IEnumerator ThrowCoworkers() {
        Vector2 diretion = new Vector2(1,1).normalized;
        float minForceStregth = 5;
        float maxForceStrength = 10;
        float maxAngularSpeed = 10;
        float maxThrowDisplacement = 5;
        float maxWaitBetweenThrows = 0.3f;

        List<BaseCoworker> coworkerSynergy = new List<BaseCoworker>();

        for (int i = 0; i < followers.Count; i++) {
            BaseCoworker follower = followers[i];
            Reaction reactionType = follower.getReactionType();
            if (reactionType == currentDialog.reaction) {
                coworkerSynergy.Add(follower);
            }
        }

        //for (int i = 0; i < coworkerSynergy.Count; i++) {
        for (int i = 0; i < 5; i++) {
            Transform clone = Instantiate(throwableCoworker).transform;
            Rigidbody2D body = clone.GetComponent<Rigidbody2D>();

            // BaseCoworker currentFollower = coworkerSynergy[i];
            // Sprite followerSprite = currentFollower.sprite;
            //clone.GetComponent<SpriteRenderer>().sprite = followerSprite;

            float displaceX = Random.Range(0, maxThrowDisplacement);
            float displaceY = Random.Range(0, maxThrowDisplacement);
            Vector2 throwDisplacement = new Vector2(displaceX, displaceY);
            Vector2 throwPosition = (Vector2)transform.position - throwDisplacement;

            Vector2 force = diretion * Random.Range(minForceStregth,maxForceStrength);
            body.AddForceAtPosition(force, throwPosition, ForceMode2D.Impulse);

            float waitTime = Random.Range(0, maxWaitBetweenThrows);
            yield return new WaitForSeconds(waitTime);
        }

    }

    void updateChoices() {
        DialogChoices currentDialog = coworker.GetInteraction();
        choice1Text.text = currentDialog.nextFirst.text;
        choice2Text.text = currentDialog.nextSecond.text;
        choice3Text.text = currentDialog.nextThird.text;
    }
    void StartInteraction() {
        UI.gameObject.SetActive(true);
        updateChoices();
    }


}
