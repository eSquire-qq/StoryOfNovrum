using UnityEngine;
using Inventory.Interaction;
using Inventory.Model;
using System.Collections.Generic;
using System.Linq;

namespace Inventory
{
    public class PickUpSystemComponent : MonoBehaviour
    {
        protected InteractionArea interactionArea;

        [SerializeField]
        protected InventoryController inventoryController;

        [SerializeField]
        protected List<AudioClip> pickupSounds;

        [SerializeField]
        protected AudioSource audioSource;

        public void Awake()
        {
            interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
        }

        public void Interact()
        {
            PickableItemObject interactionObject = interactionArea.GetCurrentItems()?.Find(x => x.GetComponent<PickableItemObject>() != null)?.GetComponent<PickableItemObject>();
            if (interactionObject != null)
            {
                if (audioSource && pickupSounds.Count() > 0)
			 	    audioSource.PlayOneShot(pickupSounds[UnityEngine.Random.Range(0, pickupSounds.Count())]);
                int reminded = inventoryController.AddItem(interactionObject.InventoryItem, interactionObject.Quantity);
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