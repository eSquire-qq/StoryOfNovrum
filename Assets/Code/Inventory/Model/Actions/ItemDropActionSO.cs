using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ItemDropActionSO : ItemActionSO
    {
        public override bool PerformAction(object actionTarget, List<ItemParameter> itemState = null)
        {
            Debug.Log(actionTarget);
            return true;
        }
    }
}