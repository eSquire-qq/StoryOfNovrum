using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

namespace Inventory.Actions
{
    [CreateAssetMenu]
    public class ItemHealActionSO : ItemActionSO
    {
        public override bool PerformAction(ActionInput input, List<ItemParameter> itemState = null)
        {
            if (itemState == null)
                return false;
            Health health = input.target.GetComponent<Health>() as Health;
            float healAmount = 0;
            healAmount = itemState.Find(x => x.itemParameter.ParameterName == "HealAmount").value;
            if (health == null || healAmount == 0)
                return false;
            health.Heal(healAmount);
            input.inventory.RemoveItem(input.itemIndex, 1);
            return true;
        }
    }
}