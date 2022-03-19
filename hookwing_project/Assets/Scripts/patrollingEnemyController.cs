using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class patrollingEnemyController : MonoBehaviour
{
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_WallCheck;
    [SerializeField] private LayerMask m_WhatIsGround;
    const float k_GroundedRadius = .2f;
    private bool m_FacingRight = false;
    public float runSpeed = 1f;
    float horizMov = 0f;
    private bool m_Grounded;
    private bool m_Walled;
    private Rigidbody2D m_Rigidbody2D;
    public Animator animator;

    public int maxHealth = 1;
    private int curHealth;

    public float m_JumpForce = 500f;
    public float kb_force = 4f;
    public float deathTime = 1.5f;


    void Awake() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
		m_Grounded = false;
        m_Walled = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
			}
		}
        if (m_Grounded == false) {
            Flip();
        }
        colliders = Physics2D.OverlapCircleAll(m_WallCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Walled = true;
			}
		}
        if (m_Walled == true) {
            Flip();
        }

        horizMov = (m_FacingRight ? 1 : -1) * runSpeed;
        Vector3 targetVelocity = new Vector2(horizMov * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = targetVelocity;
        
    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(collision.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (collision.transform.name == "Player") {
            collision.GetComponent<PlayerHealth>().DamagePlayer(1);
        }
    }

    public void takeDamage(int damage) {
        curHealth -= damage;
        Damaged();
        if (curHealth <= 0) { 
            Die();
        }
    }

    public void Damaged() {
		float knockback = (m_FacingRight ? -1 : 1) * m_JumpForce * kb_force;
		m_Rigidbody2D.velocity = new Vector2(0, 0);
		Vector3 targetVelocity = new Vector2(knockback, m_JumpForce);
		m_Rigidbody2D.AddForce(targetVelocity);
	}

    private void Die() {
        Debug.Log(name + " Died.");
        animator.SetTrigger("Dead");
        runSpeed = 1f;

        GetComponent<Collider2D>().enabled = false;
        // float curTime = Time.time;
        // while (curTime + deathTime < Time.time) {}
        Destroy(gameObject, deathTime);
        this.enabled = false;
    }
}
