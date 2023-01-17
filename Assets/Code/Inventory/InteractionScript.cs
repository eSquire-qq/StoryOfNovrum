using UnityEngine;

namespace Inverntory.Interaction
{
    public class InteractionScript : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            InteractiveItem item = collision.GetComponent<InteractiveItem>();
            if (item != null)
            {
                item.ShowHighlight();
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
    }

}
