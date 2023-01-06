using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControlledMovement : MonoBehaviour
{
	public float moveSpeed = 5f;

	public Rigidbody2D rb;
	public Animator animator;

	protected Vector2 movment;

	protected void Update()
	{
		movment.x  = Input.GetAxisRaw("Horizontal");
		movment.y  = Input.GetAxisRaw("Vertical");

		animator.SetFloat("Horizontal", movment.x);
		animator.SetFloat("Vertical", movment.y);
		animator.SetFloat("Speed", movment.sqrMagnitude);
	}

	protected void FixedUpdate()
	{
		rb.MovePosition(rb.position + movment * moveSpeed * Time.fixedDeltaTime);
	}
}
