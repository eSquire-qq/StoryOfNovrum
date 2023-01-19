using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Inverntory.Interaction;

public class PlayerControlledMovement : MonoBehaviour
{
	public float moveSpeed = 5f;

	public Rigidbody2D rb;
	public Animator animator;
	protected InteractionArea interactionArea;

	protected Vector2 movmentVector;
	protected PlayerInput playerInput;
	protected InputAction movement;
	protected InputAction interaction;

	public void Start()
	{
		interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
	}

	public void Awake()
	{
		playerInput = new PlayerInput();
	}

	public void OnEnable()
	{
		movement = playerInput.Player.Move;
		movement.Enable();

		interaction = playerInput.Player.Interact;
		interaction.Enable();
		interaction.performed += Interact;
	}

	public void OnDisable()
	{
		movement.Disable();
		interaction.Disable();
	}

	protected void Update()
	{
		movmentVector = movement.ReadValue<Vector2>();

		animator.SetFloat("Horizontal", movmentVector.x);
		animator.SetFloat("Vertical", movmentVector.y);
		animator.SetFloat("Speed", movmentVector.sqrMagnitude);
	}

	protected void FixedUpdate()
	{
		rb.MovePosition(rb.position + movmentVector * moveSpeed * Time.fixedDeltaTime);
		if (interactionArea) {
			interactionArea.transform.localPosition = movmentVector/2;
		}
	}

	protected void Interact(InputAction.CallbackContext context)
	{
		GameObject interactionObject = interactionArea.GetCurrentItem()?.gameObject;
		if (interactionObject != null)
		{
			Destroy(interactionObject);
			interactionObject = null;
		}
	}
}
