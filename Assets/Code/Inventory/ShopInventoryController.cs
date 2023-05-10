using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Inventory.Actions;

namespace Inventory
{
    public class ShopInventoryController : InventoryController
    {

        protected override void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                ActionData buyAction = item.Value.item.actionDatas.Find(x => x.actionName == GlobalConstants.Actions.BUY);
                if (buyAction != null) {
                    foreach (var action in item.Value.item.actionDatas)
                    {
                        action.visible = false;
                    }
                    buyAction.visible = true;
                    inventoryUI.UpdateData(item.Key, item.Value);
                }
            }
        }

        protected override void DropItem(int itemIndex)
        {

        }

        protected override void HandleSplitItem(int itemIndex)
        {

        }

        protected override void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            
        }
    }
}