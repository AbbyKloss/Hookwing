using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public stolen controller;
    public float runSpeed = 2f;

    float horizMov = 0f;
    bool jump = false;
    bool crouch = false;

    void Update() {
        horizMov = Input.GetAxisRaw("Horizontal") * runSpeed;
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
