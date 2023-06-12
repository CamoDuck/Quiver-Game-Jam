using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapScript : MonoBehaviour
{
    Vector3 originalScale;
    Transform outer;
    Image img;
    public FishGame gameScript;

    void Start() {
        outer = transform.Find("Outer").transform;
        img = GetComponent<Image>();

        originalScale = outer.localScale;
        
        StartCoroutine(StartAnimation());
    }
    
    IEnumerator StartAnimation() {
        const float duration = 1;
        float timer = duration;

        while (timer > 0) {
            float dt = Time.fixedDeltaTime;

            float weight = timer/duration;
            outer.localScale = Vector3.Lerp(Vector3.one, originalScale, weight);

            yield return new WaitForFixedUpdate();
            timer -= dt;
        }

        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }

    public void onClick() {
        float error = (outer.localScale - Vector3.one).magnitude;
        float maxError = (originalScale - Vector3.one).magnitude;
        float weight = error/maxError;
        Debug.Log(outer.localScale + " " + error +"/"+ maxError + " " + weight);

        if (weight < 0.3f) {
            gameScript.catchCount++;
            img.color = Color.green;
        }
        else {
            img.color = Color.red;
        }


    }

}
