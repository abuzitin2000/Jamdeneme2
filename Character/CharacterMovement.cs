using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	public float maxSpeed;
	public int health;
	public long iFrames;
	public BoxCollider2D hitbox;

	private Rigidbody2D rb;
	private Animator animator;
	private SpriteRenderer srenderer;
	private BoxCollider2D col;
	private bool attackActive = false;

	private void Awake()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator>();
		srenderer = gameObject.GetComponent<SpriteRenderer>();
		col = gameObject.GetComponent<BoxCollider2D>();
	}

	// Start is called before the first frame update
	void Start()
	{
		hitbox.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey("w"))
		{
			rb.AddForce(new Vector2(0f, 10f), ForceMode2D.Force);
		}
		if (Input.GetKey("s"))
		{

		}
		if (Input.GetKey("a") && Mathf.Abs(rb.velocity.x) < maxSpeed)
		{
			rb.AddForce(new Vector2(-5f, 0f), ForceMode2D.Force);
		}
		if (Input.GetKey("d") && Mathf.Abs(rb.velocity.x) < maxSpeed)
		{
			rb.AddForce(new Vector2(5f, 0f), ForceMode2D.Force);
		}

		if (rb.velocity.x > maxSpeed)
		{
			rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
		}

		if (rb.velocity.x < -maxSpeed)
		{
			rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
		}

		if (rb.velocity.x > 0.2f)
		{
			animator.SetBool("Running", true);
			srenderer.flipX = false;
		}
		else if (rb.velocity.x < -0.2f)
		{
			animator.SetBool("Running", true);
			srenderer.flipX = true;
		}
		else
		{
			animator.SetBool("Running", false);
		}

		if (animator.GetBool("Running"))
			animator.speed = Mathf.Abs(rb.velocity.x) / 5;
		else
			animator.speed = 1;

		if (Input.GetKey("p"))
		{
			animator.SetBool("Attack", true);
		}

		
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(iFrames <= 0)
		{
			health--;
			iFrames = 50;
		}
	}

	private void FixedUpdate()
	{
		iFrames--;
	}

	public void AttackActive()
	{
		attackActive = true;
		hitbox.enabled = true;
	}

	public void AttackEnd()
	{
		hitbox.enabled = false;
		attackActive = false;
		animator.SetBool("Attack", false);
		animator.Play("IdleAnimation");
	}
}
