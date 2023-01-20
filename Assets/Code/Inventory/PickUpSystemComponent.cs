using UnityEngine;
using Inventory.Interaction;
using Inverntory.Interaction;
using Inventory.Model;

public class PickUpSystemComponent : MonoBehaviour, IInteraction
{
    protected InteractionArea interactionArea;

    [SerializeField]
    protected InventorySO inventoryData;

    [SerializeField]
    protected IInteractionInvoker invoker;

    public void Awake()
    {
        interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
        invoker = GetComponent(typeof(IInteractionInvoker)) as IInteractionInvoker;
        invoker.OnInteraction += Interact;
    }

    public void Interact(object interactionContext)
    {
        Item interactionObject = interactionArea.GetCurrentItem().GetComponent<Item>();
        Debug.Log(interactionObject);
		if (interactionObject != null)
		{
            int reminded = inventoryData.AddItem(interactionObject.InventoryItem, interactionObject.Quantity);
            if (reminded == 0) {
                interactionObject.DestroyItem();
                Destroy(interactionObject.gameObject);
			    interactionObject = null;
            }
            else {
                interactionObject.Quantity = reminded;
            }
		}
    }
}
