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
    [SerializeField] new Collider2D collider;
    [SerializeField] GameObject throwableCoworker;

    /// VARYING ///
    BaseCoworker coworker; // current interacting coworker

    public void onDialogClick(int choice) {
        StartCoroutine(ThrowCoworkers());
        coworker.TryInteraction(choice);
        updateChoices();
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Coworker") {
            coworker = other.GetComponent<BaseCoworker>();
            StartInteraction();
        }
    }

    IEnumerator ThrowCoworkers() {
        Vector2 diretion = new Vector2(1,1).normalized;
        float minForceStregth = 5;
        float maxForceStrength = 10;
        float maxAngularSpeed = 10;
        float maxThrowDisplacement = 5;
        float maxWaitBetweenThrows = 0.3f;

        int count = 5;
        for (int i = 0; i < count; i++) {
            Transform clone = Instantiate(throwableCoworker).transform;
            Rigidbody2D body = clone.GetComponent<Rigidbody2D>();

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
        string[] dialog = coworker.GetInteraction();
        choice1Text.text = dialog[0];
        choice2Text.text = dialog[1];
        choice3Text.text = dialog[2];
    }
    void StartInteraction() {
        UI.gameObject.SetActive(true);
        updateChoices();
    }


}
