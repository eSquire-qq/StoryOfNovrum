using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public abstract class ItemActionSO : ScriptableObject, IItemAction
    {
        public abstract bool PerformAction(object actionTarget, List<ItemParameter> itemState = null);
    }
}