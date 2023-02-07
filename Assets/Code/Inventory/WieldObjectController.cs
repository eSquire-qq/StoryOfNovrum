using System.Linq;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;

namespace Inventory
{
    public class WieldObjectController : MonoBehaviour
    {
        [SerializeField]
        protected UIInventoryPage inventoryUI;

        [SerializeField]
        protected InventorySO inventoryData;

        [SerializeField]
        protected InventoryItem deafultItem;

        public InventoryItem wieldItem { get;  protected set; }

        private void Start()
        {
            if (inventoryUI) {
                inventoryUI.OnSelect += HandleSelect;
                inventoryUI.OnUnselect += HandleUnselect;
            }
            wieldItem = InventoryItem.GetEmptyItem();
            if (!deafultItem.IsEmpty) {
                deafultItem.itemState = (deafultItem.itemState != null && deafultItem.itemState.Count() > 0) ? deafultItem.itemState : deafultItem.item.DefaultParametersList;
                wieldItem = deafultItem;
            }
        }

        protected void Update()
        {
            ItemParameter durability = wieldItem.itemState.Find(x => x.itemParameter.ParameterName == "Durability");
            if (durability.itemParameter && durability.value <= 0f && inventoryData)
            {
                    int brokenItemIndex = inventoryData.GetIndex(wieldItem);
                    HandleUnselect(brokenItemIndex);
                    inventoryData.RemoveItem(brokenItemIndex, wieldItem.quantity);
            }
        }

        protected void HandleSelect(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                return;
            }
            ItemSO item = inventoryItem.item;
            wieldItem = inventoryItem;
        }

        protected void HandleUnselect(int itemIndex)
        {
            wieldItem = deafultItem.IsEmpty ? InventoryItem.GetEmptyItem() : deafultItem;
            return;
        }
    }
}