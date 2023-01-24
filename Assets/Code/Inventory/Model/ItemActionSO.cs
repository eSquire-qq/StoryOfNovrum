using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

namespace Inventory.Actions
{
    [CreateAssetMenu]
    public abstract class ItemActionSO : ScriptableObject, IItemAction
    {
        public abstract bool PerformAction(ActionInput input, List<ItemParameter> itemState = null);
    }
}