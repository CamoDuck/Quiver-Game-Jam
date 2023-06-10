using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableCoworker : MonoBehaviour
{
    /// CONSTANT ///

    /// VARYING
    float lifeTime = 10; // seconds


    void Update() {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) {
            Destroy(transform);
        }
    }
}
