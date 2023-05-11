using System;
using UnityEngine;

namespace Inventory.Interaction
{
    public class InteractiveObject : MonoBehaviour
    {
        public event Action<object> OnInteraction;

        public void Interact(object context)
        {
            OnInteraction?.Invoke(context);
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