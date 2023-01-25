using Inventory.Model;
using UnityEngine;

namespace Inverntory.Interaction
{
    public class InteractionArea : MonoBehaviour
    {
        [SerializeField]
        protected GameObject currentItem;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject item = collision.gameObject;
            if (item == null) {
                return;
            }
            currentItem = item;
            InteractiveObject interativeItem = currentItem.GetComponent<InteractiveObject>();
            if (interativeItem != null)
            {
                interativeItem.ShowHighlight();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            GameObject item = collision.GetComponent<GameObject>();
            if (currentItem == item) {
                currentItem = null;
            }
            InteractiveObject interativeItem = currentItem.GetComponent<InteractiveObject>();
            if (interativeItem != null)
            {
                interativeItem.HideHighlight();
            }
        }

        public GameObject GetCurrentItem()
        {
            return currentItem;
        }
    }

}
