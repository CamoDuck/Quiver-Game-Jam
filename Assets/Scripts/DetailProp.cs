using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailProp : MonoBehaviour
{
    new Collider2D collider;
    PlayerUI playerScript;

    void Start() {
        collider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            collider.enabled = false;
            playerScript = other.transform.GetComponent<PlayerUI>();
            StartCoroutine(StartAnimation());
        }
    }

    IEnumerator StartAnimation() {
        const float animationDuration = 1;
        float animationTimer = animationDuration;
        float totalRotation = 360; //degrees
        Vector2 originalPosition = transform.position;
        Vector2 destination = playerScript.transform.position;

        playerScript.setMovementEnabled(false);

    
        
        while (animationTimer > 0) {
            float dt = Time.fixedDeltaTime;

            float weight = animationTimer/animationDuration;
            transform.position = Vector2.Lerp(destination, originalPosition, weight);
            transform.localScale -= Vector3.one * (dt / animationDuration);

            float angleChange = totalRotation * dt / animationDuration;
            transform.Rotate(Vector3.forward * angleChange);

            animationTimer -= dt;
            yield return new WaitForFixedUpdate();
        }

        playerScript.setMovementEnabled(true);
    }

    
}
