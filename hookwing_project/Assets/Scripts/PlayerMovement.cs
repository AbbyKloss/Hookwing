using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public stolen controller;
    public Animator animator;
    public float runSpeed = 2f;

    float horizMov = 0f;
    bool jump = false;
    bool crouch = false;

    void Update() {
        horizMov = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizMov));
        // vertiMov = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }
    }

    void FixedUpdate() {
        controller.Move(horizMov, crouch, jump);
        jump = false;
    }
}
