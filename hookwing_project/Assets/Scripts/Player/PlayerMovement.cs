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
    bool attack = false;
    public bool grapple = false;

    void Update() {
        horizMov = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizMov));
        // vertiMov = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }
        if (Input.GetMouseButtonDown(1)) {
            attack = true;
        }
        grapple = ((GetComponent<grapple>().grappled) || (GetComponent<grappleZip>().grappled));
    }

    void FixedUpdate() {
        // string something0 = grapple ? "s0True" : "s0False";
        // string something1 = GetComponent<grapple>().grappled ? "s1True" : "s1False";
        // string something2 = GetComponent<grappleZip>().grappled ? "s2True" : "s2False";
        // Debug.Log(something0 + " " + something1 + " " + something2 + " " + Time.time);
        controller.Move(horizMov, crouch, jump, attack, grapple);
        jump = false;
        attack = false;
        grapple = false;
    }
}
