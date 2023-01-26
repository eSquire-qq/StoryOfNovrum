using Inventory.Model;
using UnityEngine;

namespace Inventory.Interaction
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

        private void OnTriggerStay2D(Collider2D collision)
        {
            GameObject item = collision.gameObject;
            if (item == null) {
                return;
            }
            currentItem = item;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            GameObject item = collision.gameObject;
            InteractiveObject interativeItem = item.GetComponent<InteractiveObject>();
            if (interativeItem != null)
            {
                interativeItem.HideHighlight();
            }
            if (GameObject.ReferenceEquals(currentItem, item)) {
                currentItem = null;
            }
        }

        public GameObject GetCurrentItem()
        {
            return currentItem;
        }
    }

}
