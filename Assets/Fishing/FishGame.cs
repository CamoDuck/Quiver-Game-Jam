using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishGame : MonoBehaviour
{
    [SerializeField] GameObject tapSpot;
    [SerializeField] GameObject dialogBox;
    [SerializeField] TextMeshProUGUI dialogText;
    RectTransform gameUI;
    float UIWidth;
    float UIHeight;
    new Collider2D collider;

    public int catchCount = 0;

    bool enabled = true;

    void Start() {
        collider = GetComponent<Collider2D>();
        gameUI = (RectTransform)transform.Find("Canvas");

        UIHeight = gameUI.rect.height;
        UIWidth = gameUI.rect.width;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            StartCoroutine(StartFishing());
            enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            enabled = true;
        }
    }

    IEnumerator StartFishing() {
        const float duration = 10;
        float timer = duration;

        while (timer > 0) {
            SpawnCircle();

            float waitTime = Random.Range(0.5f,1f);
            yield return new WaitForSeconds(waitTime);
            timer -= waitTime;
        }

        dialogText.text = "You caught " + catchCount + " fish!";
        dialogBox.SetActive(true);

        yield return new WaitForSeconds(3f);

        dialogBox.SetActive(false);
    }   

    void SpawnCircle() {
        float randX = Random.Range(0, UIWidth);
        float randY = Random.Range(0, UIHeight);
        Vector2 location = new Vector2(randX, randY);

        Transform clone = Instantiate(tapSpot).transform;
        clone.SetParent(gameUI);
        clone.position = location;
        clone.GetComponent<TapScript>().gameScript = this;
    }
}
