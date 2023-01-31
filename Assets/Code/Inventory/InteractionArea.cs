using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

namespace Inventory.Interaction
{
    public class InteractionArea : MonoBehaviour
    {
        [SerializeField]
        protected List<GameObject> currentItems;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject item = collision.gameObject;
            if (item == null) {
                return;
            }
            currentItems.Add(item);
            InteractiveObject interativeItem = item.GetComponent<InteractiveObject>();
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
            if (!currentItems.Contains(item)) {
                currentItems.Add(item);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            GameObject item = collision.gameObject;
            InteractiveObject interativeItem = item.GetComponent<InteractiveObject>();
            if (interativeItem != null)
            {
                interativeItem.HideHighlight();
            }
            if (currentItems.Contains(item)) {
                currentItems.Remove(item);
            }
        }

        public List<GameObject> GetCurrentItems()
        {
            return currentItems;
        }
    }

}
