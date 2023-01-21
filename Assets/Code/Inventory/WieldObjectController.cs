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

        protected SpriteRenderer wieldedObjectSprite;

        private void Start()
        {
            inventoryUI.OnSelect += HandleSelect;
            inventoryUI.OnUnselect += HandleUnselect;
            wieldedObjectSprite = GetComponent<SpriteRenderer>() as SpriteRenderer;
        }

        protected void HandleSelect(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                wieldedObjectSprite.sprite = null;
                return;
            }
            ItemSO item = inventoryItem.item;
            wieldedObjectSprite.sprite = item.ItemImage;
        }

        protected void HandleUnselect(int itemIndex)
        {
            wieldedObjectSprite.sprite = null;
            return;
        }
    }
}