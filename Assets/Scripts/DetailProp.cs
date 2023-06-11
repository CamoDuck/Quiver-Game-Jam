using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailProp : MonoBehaviour
{
    public Animator animator;
    public string playerTag;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != playerTag) return;

        animator.SetBool("triggered", true);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag != playerTag) return;

        animator.SetBool("triggered", false);        
    }
    
}
