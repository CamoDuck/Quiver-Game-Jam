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
    [SerializeField] Collider2D collider;

    /// VARYING ///
    BaseCoworker coworker; // current interacting coworker


    void Start() {

    }

    public void onDialogClick(int choice) {
        coworker.TryInteraction(choice);
        updateChoices();
    }

    void OnTriggerEnter2D (Collider2D other) {
        Debug.Log(other);
        if (other.tag == "Coworker") {
            coworker = other.GetComponent<BaseCoworker>();
            Debug.Log("ran");
            StartInteraction();
        }
    }

    void updateChoices() {
        string[] dialog = coworker.GetInteraction();
        choice1Text.text = dialog[0];
        choice2Text.text = dialog[1];
        choice3Text.text = dialog[2];
    }
    void StartInteraction() {
        Debug.Log("start");
        UI.gameObject.SetActive(true);
        updateChoices();

    }


}
