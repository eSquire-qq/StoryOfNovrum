using Inventory.Model;
using UnityEngine;

namespace Inverntory.Interaction
{
    public class InteractionArea : MonoBehaviour
    {
        [SerializeField]
        protected InteractiveObject currentItem;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            InteractiveObject item = collision.GetComponent<InteractiveObject>();
            if (item != null)
            {
                item.ShowHighlight();
                currentItem = item;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            InteractiveObject item = collision.GetComponent<InteractiveObject>();
            if (item != null)
            {
                item.HideHighlight();
                if (currentItem == item) {
                    currentItem = null;
                }
            }
        }

        public InteractiveObject GetCurrentItem()
        {
            return currentItem;
        }
    }

}
