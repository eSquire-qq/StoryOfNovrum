using UnityEngine;
using UnityEngine.InputSystem;
using Inverntory.Interaction;
using System;
using Inventory.Interaction;

public class PlayerControlledMovement : MonoBehaviour, IInteractionInvoker
{

	public float horizontal;
    public float vertical;

    protected bool isRunning;
    private Vector3 moveDir;

	public float moveSpeed = 100f;

	public Rigidbody2D rb;
	public Animator animator;
	protected InteractionArea interactionArea;

	protected Vector3 movmentVector;
	protected PlayerInput playerInput;

	protected InputAction movement;
	protected InputAction interaction;
	public event Action<object> OnInteraction;

    public void Start()
	{
		interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
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
		horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(horizontal != 0 || vertical != 0)
        {
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);    
            if(!isRunning)
            {
                isRunning = true;
                animator.SetBool("isRunning", isRunning);
            }
        }else
        {
            if(isRunning)
            {
                isRunning = false;
                animator.SetBool("isRunning",isRunning);
                StopRunning();
            }
        }
        moveDir = new Vector3(horizontal,vertical).normalized;
	}

	protected void FixedUpdate()
	{
		rb.velocity = moveDir * moveSpeed * Time.deltaTime;
		if (interactionArea) {
			interactionArea.transform.localPosition = movmentVector/2;
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
