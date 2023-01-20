using UnityEngine;

namespace Inverntory.Interaction
{
    public class InteractiveItem : MonoBehaviour
    {
        protected SpriteRenderer sprRend;

        void Start()
        {
            sprRend = gameObject.GetComponent<SpriteRenderer>();
        }

        public void ShowHighlight() 
        {
            transform.localScale = new Vector2(1.1f, 1.1f);
            gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineEnabled", 1f);
        }

        public void HideHighlight() 
        {
            transform.localScale = new Vector2(1f, 1f);
            gameObject.GetComponent<Renderer>().material.SetFloat("_OutlineEnabled", 0f);
        }

    }
}