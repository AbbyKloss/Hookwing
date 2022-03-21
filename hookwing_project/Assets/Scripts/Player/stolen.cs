using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class stolen : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 1200f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private LayerMask m_WhatIsWall;
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_WallCheck;
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	const float k_wallRadius = 1f;
	public bool m_Wall;

	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	public Animator animator;
	public GameObject deathMenu;
    public LayerMask enemyLayers;

	public int num_of_jumps = 2;
	public int kb_force = 2;

	
	public int jumpNum;

	[Header("Events")]
	[Space]

	private bool m_wasCrouching = false;
	private bool isDead = false;
	public bool playerDead;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		playerDead = isDead;
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		m_Wall = false;
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
		Collider2D[] collidersWall = Physics2D.OverlapCircleAll(m_WallCheck.position, k_wallRadius, m_WhatIsWall);
		for (int i = 0; i < collidersWall.Length; i++)
		{
			if (collidersWall[i].gameObject != gameObject && !m_Grounded)
			{
				m_Wall = true;
			}
			
				
		}
	}


	public void Move(float move, bool crouch, bool jump, bool attack, bool grapple)
	{
		if (isDead) return;
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if ((jumpNum > 0) && jump) {
			m_Grounded = false;
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			if(!m_Wall)
			jumpNum--;
		}
		else if(jump && m_Wall)
        {
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
		
		if ((jumpNum < num_of_jumps) && m_Grounded) {
			jumpNum = num_of_jumps;
		}

		if (attack) {
			GetComponent<PlayerAttack>().Attack();
		}

		if (grapple) {			
			animator.SetTrigger("Grapple");
		}
		// else animator.SetBool("Grapple", false);
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

	public void Damaged() {
		float knockback = (m_FacingRight ? -1 : 1) * m_JumpForce * kb_force;
		m_Rigidbody2D.velocity = new Vector2(0, 0);
		Vector3 targetVelocity = new Vector2(knockback, m_JumpForce);
		m_Rigidbody2D.AddForce(targetVelocity);
	}

	public void Die() {
		isDead = true;
        animator.SetBool("Death", true);
		float temp = Time.time;
		float dietime = 0.1f;
		GameObject.FindWithTag("Player").transform.rotation = Quaternion.Euler(Vector3.forward * (m_FacingRight ? 1 : -1) * 90);
		StartCoroutine(StopFlying(dietime));
		StartCoroutine(DeathMenu());
    }

	IEnumerator StopFlying(float time) {
		yield return new WaitForSeconds(time);
		m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
	}
	IEnumerator DeathMenu() {
		yield return new WaitForSeconds(0.75f);
		deathMenu.GetComponent<DeathMenu>().Arise();
	}
}
	