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
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        protected UIInventoryPage inventoryUI;

        [SerializeField]
        protected InventorySO inventoryData;

        [SerializeField]
        protected GameObject ItemPrefab;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        protected virtual void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        protected virtual void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }

        protected virtual void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value);
            }
        }

        protected void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnSelect += HandleSelect;
            inventoryUI.OnUnselect += HandleUnselect;
            inventoryUI.OnSplit += HandleSplitItem;
            inventoryUI.OnDrop += DropItem;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
            inventoryUI.Show();
        }

        public virtual int AddItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            return inventoryData.AddItem(item, quantity, itemState);
        }

        protected virtual void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            if (inventoryItem.item.actionDatas.Count <= 0)
                return;

            inventoryData.InformAboutChange();
            inventoryUI.ShowItemAction(itemIndex);
            foreach(ActionData action in inventoryItem.item.actionDatas)
            {
                if (!action.visible) continue;
                inventoryUI.AddAction(action.actionName, () => {
                    bool success = action.action.PerformAction(new ActionInput{
                        target = this.gameObject,
                        inventory = this.inventoryData,
                        itemIndex = itemIndex
                    }, inventoryItem.itemState);
                    if (success) {
                        inventoryUI.HideItemAction();
                        if (inventoryData.GetItemAt(itemIndex).IsEmpty) {
                            inventoryUI.DeselectItem(itemIndex);
                            inventoryUI.DeselectAllItems();
                        }
                    }
                });
            }
        }

        protected virtual void DropItem(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            ActionData dropAction = inventoryItem.item.actionDatas.Find(x => x.actionName == GlobalConstants.Actions.DROP);
            if (dropAction == null)
                return;
            bool success = dropAction.action.PerformAction(new ActionInput{
                        target = this.gameObject,
                        inventory = this.inventoryData,
                        itemIndex = itemIndex
                    }, inventoryItem.itemState);
            if (success) {
                inventoryUI.HideItemAction();
                if (inventoryData.GetItemAt(itemIndex).IsEmpty) {
                    inventoryUI.DeselectItem(itemIndex);
                    inventoryUI.DeselectAllItems();
                }
            }
        }

        protected virtual void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem);
        }

        protected virtual void HandleSplitItem(int itemIndex)
        {
            inventoryData.SplitItem(itemIndex);
        }

        protected virtual void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        protected virtual void HandleSelect(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.DeselectAllItems();
                return;
            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage,
                item.name, description);
        }

        protected virtual void HandleUnselect(int itemIndex)
        {
                inventoryUI.DeselectAllItems();
                return;
        }

        protected string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName} " +
                    $": {inventoryItem.itemState[i].value} / " +
                    $"{inventoryItem.item.DefaultParametersList[i].value}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public void Update()
        {
        
        }
    }
}