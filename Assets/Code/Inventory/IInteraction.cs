using System;
using Inverntory.Interaction;

namespace Inventory.Interaction
{
    public interface IInteraction {
        public void Interact(object context);
    }

    public interface IInteractionInvoker {
        public event Action<object> OnInteraction;
    }
}