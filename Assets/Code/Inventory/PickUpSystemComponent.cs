using UnityEngine;
using Inventory.Interaction;
using Inventory.Model;

namespace Inventory
{
    public class PickUpSystemComponent : MonoBehaviour
    {
        protected InteractionArea interactionArea;

        [SerializeField]
        protected InventorySO inventoryData;

        public void Awake()
        {
            interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
        }

        public void Interact()
        {
            PickableItemObject interactionObject = interactionArea.GetCurrentItems()?.Find(x => x.GetComponent<PickableItemObject>() != null)?.GetComponent<PickableItemObject>();
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