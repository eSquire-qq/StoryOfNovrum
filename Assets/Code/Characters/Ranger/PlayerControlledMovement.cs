using UnityEngine;
using UnityEngine.InputSystem;
using Inverntory.Interaction;
using System;
using Inventory.Interaction;

public class PlayerControlledMovement : MonoBehaviour, IInteractionInvoker
{
	public float moveSpeed = 5f;

	public Rigidbody2D rb;
	public Animator animator;
	protected InteractionArea interactionArea;

	protected Vector2 movmentVector;
	protected PlayerInput playerInput;

	protected InputAction movement;
	protected InputAction interaction;
	protected InputAction attack;

	public event Action<object> OnInteraction;

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
		attack = playerInput.Player.Attack;
		attack.Enable();
		attack.performed += Interact;

		movement = playerInput.Player.Move;
		movement.Enable();

		interaction = playerInput.Player.Interact;
		interaction.Enable();
		interaction.performed += Interact;
	}

	public void OnDisable()
	{
		attack.Disable();
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

	public void Interact(InputAction.CallbackContext context)
	{
		OnInteraction?.Invoke(context);
	}
}
