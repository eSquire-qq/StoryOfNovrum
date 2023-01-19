using UnityEngine;

namespace Inverntory.Interaction
{
    public class InteractiveItem : Item
    {
        protected SpriteRenderer sprRend;

        void Start()
        {
            sprRend = gameObject.GetComponent<SpriteRenderer>();
        }

        public void ShowHighlight() 
        {
            transform.localScale = new Vector2(1.1f, 1.1f);
            gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_OutlineEnabled", 1f);
        }

        public void HideHighlight() 
        {
            transform.localScale = new Vector2(1f, 1f);
            gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_OutlineEnabled", 0f);
        }

    }
}