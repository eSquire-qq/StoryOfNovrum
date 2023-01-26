using System;

namespace Inventory.Interaction
{
    public interface IInteraction {
        public void Interact(object context);
    }

    public interface IInteractionInvoker<T> {
        public event Action<T> OnInteraction;
    }
}