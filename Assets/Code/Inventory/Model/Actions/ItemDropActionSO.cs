using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

namespace Inventory.Actions
{
    [CreateAssetMenu]
    public class ItemDropActionSO : ItemActionSO
    {
        [SerializeField]
        protected GameObject ItemPrefab;

        public override bool PerformAction(ActionInput input, List<ItemParameter> itemState = null)
        {
            GameObject dropItem = Instantiate(ItemPrefab) as GameObject;
            InventoryItem item = input.inventory.GetItemAt(input.itemIndex);
            dropItem.GetComponent<PickableItemObject>().InventoryItem = item.item;
            dropItem.GetComponent<PickableItemObject>().Quantity = item.quantity;
            dropItem.transform.position = input.target.transform.position;
            input.inventory.RemoveItem(input.itemIndex, item.quantity);
            return true;
        }
    }
}