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
    public class PlayerInventoryController : InventoryController
    {

        [SerializeField]
        protected GoldCounter goldCounter;

        protected override void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                ActionData buyAction = item.Value.item.actionDatas.Find(x => x.actionName == GlobalConstants.Actions.BUY);
                foreach (var action in item.Value.item.actionDatas)
                {
                    action.visible = true;
                }
                buyAction.visible = false;
                inventoryUI.UpdateData(item.Key, item.Value);
            }
        }

        protected override void Start()
        {
            base.Start();
            goldCounter = GetComponent(typeof(GoldCounter)) as GoldCounter;
        }

        public override int AddItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            if (item.name == "GoldBagItem" && goldCounter) {
                goldCounter.AddGold(quantity);
                return 0;
            }
            return base.AddItem(item, quantity, itemState);
        }

    }
}