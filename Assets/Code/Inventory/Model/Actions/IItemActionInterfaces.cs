using Inventory.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Actions
{
    public struct ActionInput
    {
        public GameObject target;
        public InventorySO inventory;
        public int itemIndex;
    }

    public interface IItemAction
    {
        bool PerformAction(ActionInput input, List<ItemParameter> itemState);
    }

    public interface IItemWithActions
    {
        bool PerformAction(ActionInput input, string actionName);
    }
}