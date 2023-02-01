using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Inventory.Interaction;
using Animations;

public class PlayerControlledMovement : MonoBehaviour, IInteractionInvoker<object>
{

    protected Vector3 moveDir;

	public float moveSpeed = 100f;

	public Rigidbody2D rb;
	public AnimatorController animatorController;
	protected InteractionArea interactionArea;
	protected PlayerInput playerInput;

	protected InputAction movement;
	protected InputAction interaction;
	public event Action<object> OnInteraction;

    public void Start()
	{
		interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
		animatorController = GetComponent(typeof(AnimatorController)) as AnimatorController;
		rb = GetComponent<Rigidbody2D>();
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
		float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal == 0 && vertical == 0)
        {
			animatorController.ChangeAnimationState("Idle", animatorController.currentAnimationState == "Run" ? true : false);
        } else {
			animatorController.ChangeAnimationState("Run");
		}

		moveDir = new Vector3(horizontal,vertical).normalized;
		if (moveDir.x < 0 || moveDir.x > 0) {
			transform.localScale = new Vector2(moveDir.x < 0 ? 1 : -1, 1f);
		}
	}

	protected void FixedUpdate()
	{
		rb.velocity = moveDir * moveSpeed * Time.deltaTime;
		if (interactionArea) {
			if (moveDir.x != 0 || moveDir.y != 0) {
				interactionArea.transform.position = transform.position + moveDir/2;
			}
		}
	}

	public void Interact(InputAction.CallbackContext context)
	{
		OnInteraction?.Invoke(context);
	}

	private void StopRunning()
    {
        rb.velocity = Vector3.zero;
    }

}
