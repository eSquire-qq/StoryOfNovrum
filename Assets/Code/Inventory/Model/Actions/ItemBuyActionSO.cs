using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using System;

namespace Inventory.Actions
{
    [CreateAssetMenu]
    public class ItemBuyActionSO : ItemActionSO
    {
        public override bool PerformAction(ActionInput input, List<ItemParameter> itemState = null)
        {
            GameObject player = (input.target.GetComponent(typeof(TraderController)) as TraderController)?.player;
            if (player == null) return false;
            GoldCounter playerGoldCounter = player?.GetComponent(typeof(GoldCounter)) as GoldCounter;
            PlayerInventoryController playerInventory = player?.GetComponent(typeof(PlayerInventoryController)) as PlayerInventoryController;
            int cost = Convert.ToInt32(itemState.Find(x => x.itemParameter.ParameterName == "Cost").value);
            Debug.Log(playerGoldCounter);
            if (playerGoldCounter != null && cost != null) {
                Debug.Log(playerGoldCounter.currentGold);
                if (playerGoldCounter.currentGold >= cost) {
                    playerGoldCounter.RemoveGold(cost);
                    playerInventory.AddItem(input.inventory.GetItemAt(input.itemIndex).item, 1, itemState);
                }
            }
            return true;
        }
    }
}