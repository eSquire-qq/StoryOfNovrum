using Inventory.Model;
using UnityEngine;

namespace Inverntory.Interaction
{
    public class InteractionArea : MonoBehaviour
    {
        [SerializeField]
        protected InteractiveItem currentItem;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            InteractiveItem item = collision.GetComponent<InteractiveItem>();
            if (item != null)
            {
                item.ShowHighlight();
                currentItem = item;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            InteractiveItem item = collision.GetComponent<InteractiveItem>();
            if (item != null)
            {
                item.HideHighlight();
            }
        }

        public InteractiveItem GetCurrentItem()
        {
            return currentItem;
        }
    }

}
