using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	public float minGroundNormalY = .65f;
	public float gravityModifier = 1f;

	Vector2 targetVelocity;
	bool grounded;
	Vector2 groundNormal;
	Rigidbody2D rb2d;
	Vector2 velocity;
	ContactFilter2D contactFilter;
	RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

	float minMoveDistance = 0.001f;
	float shellRadius = 0.01f;

	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 7;

	private SpriteRenderer spriteRenderer;
	private Animator animator;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}

	void OnEnable()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		contactFilter.useTriggers = false;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
		contactFilter.useLayerMask = true;
	}

	void Update()
	{
		targetVelocity = Vector2.zero;
		ComputeVelocity();
	}

	private void ComputeVelocity()
	{
		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis("Horizontal");

		if (Input.GetButtonDown("Jump") && grounded)
		{
			velocity.y = jumpTakeOffSpeed;
		}
		else if (Input.GetButtonUp("Jump"))
		{
			if (velocity.y > 0)
			{
				velocity.y = velocity.y * 0.5f;
			}
		}

		bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
		if (flipSprite)
		{
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		animator.SetBool("grounded", grounded);
		//animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

		targetVelocity = move * maxSpeed;
	}

	void FixedUpdate()
	{
		velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
		velocity.x = targetVelocity.x;

		grounded = false;

		Vector2 deltaPosition = velocity * Time.deltaTime;

		Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

		Vector2 move = moveAlongGround * deltaPosition.x;

		Movement(move, false);

		move = Vector2.up * deltaPosition.y;

		Movement(move, true);
	}

	void Movement(Vector2 move, bool yMovement)
	{
		float distance = move.magnitude;

		if (distance > minMoveDistance)
		{
			int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
			hitBufferList.Clear();
			for (int i = 0; i < count; i++)
			{
				hitBufferList.Add(hitBuffer[i]);
			}

			for (int i = 0; i < hitBufferList.Count; i++)
			{
				Vector2 currentNormal = hitBufferList[i].normal;
				if (currentNormal.y > minGroundNormalY)
				{
					grounded = true;
					if (yMovement)
					{
						groundNormal = currentNormal;
						currentNormal.x = 0;
					}
				}

				float projection = Vector2.Dot(velocity, currentNormal);
				if (projection < 0)
				{
					velocity = velocity - projection * currentNormal;
				}

				float modifiedDistance = hitBufferList[i].distance - shellRadius;
				distance = modifiedDistance < distance ? modifiedDistance : distance;
			}


		}

		rb2d.position = rb2d.position + move.normalized * distance;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Nut")
		{
			VideoManager.instance.StartVideo();
		}
	}
}