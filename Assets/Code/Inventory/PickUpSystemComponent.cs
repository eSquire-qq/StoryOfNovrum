using UnityEngine;
using Inventory.Interaction;
using Inverntory.Interaction;
using Inventory.Model;

namespace Inventory
{
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
            PickableItemObject interactionObject = interactionArea.GetCurrentItem()?.GetComponent<PickableItemObject>();
            if (interactionObject != null)
            {
                int reminded = inventoryData.AddItem(interactionObject.InventoryItem, interactionObject.Quantity);
                if (reminded == 0) {
                    interactionObject.DestroyItem();
                    Destroy(interactionObject.gameObject);
                }
                else {
                    interactionObject.Quantity = reminded;
                }
            }
        }
    }
}