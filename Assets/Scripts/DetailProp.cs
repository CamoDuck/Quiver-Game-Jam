using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailProp : MonoBehaviour
{
    public Animator animator;

    void OnTriggerEnter(Collider collider)
    {
        animator.enabled = true;
    }
    
}
