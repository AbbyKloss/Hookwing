using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackFX : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        // GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Attack() {
        // GetComponent<SpriteRenderer>().enabled = true;

        animator.SetTrigger("Attack");
        Debug.Log("Point attacked.");

        // GetComponent<SpriteRenderer>().enabled = false;
    }
}
