using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailProp : MonoBehaviour
{
    public Animator animator;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("trigger");
        animator.enabled = true;
    }

    void OnTriggerExit()
    {
        animator.enabled = false;
    }
    
}
