using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Inventory.Interaction;
using Inventory;
using UnityEngine.UI;
using UnityEngine.UI;

public class PlayerInteractionManager : MonoBehaviour
{

	public int goldCount;
	public Text goldCountText;

	[SerializeField]
	protected InteractionArea interactionArea;

	[SerializeField]
	protected PlayerController playerController;

	[SerializeField]
	protected SimpleMeleeAttackComponent attackComponent;

	[SerializeField]
	protected PickUpSystemComponent pickUpComponent;

	public void Awake()
	{
		playerController = GetComponent(typeof(PlayerController)) as PlayerController;
		if (playerController != null) {
			playerController.OnInteraction += Interact;
		}
		interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
	}

	public void Interact(object context)
	{
		Health attackObject = interactionArea.GetCurrentItems()?.Find(x => (x.GetComponent<Health>() != null && x != gameObject))?.GetComponent<Health>();
		if (attackObject && attackComponent) {
			attackComponent.Attack();
			return;
		}
		PickableItemObject interactionObject = interactionArea.GetCurrentItems()?.Find(x => x.GetComponent<PickableItemObject>() != null)?.GetComponent<PickableItemObject>();
		if (interactionObject && pickUpComponent) {
			pickUpComponent.Interact();
			return;
		}
		if (attackComponent) {
			attackComponent.Attack();
		}
	}
}
