using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public interface IDestroyableItem
    {

    }

    public interface IItemAction
    {
        bool PerformAction(object actionTarget, List<ItemParameter> itemState);
    }

    public interface IItemWithActions
    {
        bool PerformAction(object actionTarget, string actionName);
    }
    
}