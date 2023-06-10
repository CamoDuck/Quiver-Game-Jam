using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    /// CONSTANT ///
    [SerializeField] Transform player;
    const float maxMoveSpeed = 5;
    const float minMoveSpeed = 1.5f;
    const float maxDist = 5;

    /// VARYING ///

    void FixedUpdate() {
        Vector2 playerDistVector = player.position - transform.position;
        Vector2 playerDirection = playerDistVector.normalized;
        float playerDist = playerDistVector.magnitude;

        float moveSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, playerDist/maxDist) * Time.fixedDeltaTime;

        moveSpeed = moveSpeed > playerDist? playerDist: moveSpeed;
        transform.Translate(playerDirection * moveSpeed);
    }
}
