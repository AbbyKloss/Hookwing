using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {

	public float speed = 5.0f;
	private bool facingRight = true;
	public float jumpForce = 500.0f;

	private Rigidbody2D rigidBody2D;

	void Start() {
		rigidBody2D = GetComponent<Rigidbody2D>();
	}

	void Update() {

	}

	void FixedUpdate() {
		// float h = 0.0f;
		float v = 0.0f;
		// if (Input.GetKey("w")) { v = 1.0f; }
		// if (Input.GetKey("s")) { v = -1.0f; }
		// if (Input.GetKey("a")) { h = -1.0f; }
		// if (Input.GetKey("d")) { h = 1.0f; }

		float h = Input.GetAxis("Horizontal");
		// float v = Input.GetAxis("Vertical");
		if (Input.GetKey("space")) { rigidBody2D.AddForce(new Vector2(0, jumpForce) * speed);}

		// rigidBody2D.AddForce(new Vector2(h, v) * speed);
		rigidBody2D.MovePosition(rigidBody2D.position + (new Vector2(h, v) * speed * Time.fixedDeltaTime));
	}
}