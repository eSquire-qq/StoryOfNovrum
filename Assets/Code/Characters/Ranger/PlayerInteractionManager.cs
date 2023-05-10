using UnityEngine;
using Inventory.Interaction;
using Inventory;

public class PlayerInteractionManager : MonoBehaviour
{

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
		PickableItemObject pickableObject = interactionArea.GetCurrentItems()?.Find(x => x.GetComponent<PickableItemObject>() != null)?.GetComponent<PickableItemObject>();
		if (pickableObject && pickUpComponent) {
			pickUpComponent.Interact();
			return;
		}
		InteractiveObject interactionObject = interactionArea.GetCurrentItems()?.Find(x => x.GetComponent<InteractiveObject>() != null)?.GetComponent<InteractiveObject>();
		if (interactionObject) {
			interactionObject.Interact(context);
			return;
		}
		if (attackComponent) {
			attackComponent.Attack();
			return;
		}
	}

}
