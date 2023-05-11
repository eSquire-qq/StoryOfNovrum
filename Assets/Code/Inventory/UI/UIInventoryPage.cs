using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine;
using Inventory.Model;
using TMPro;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField]
        protected UIInventoryItem itemPrefab;

        [SerializeField]
        protected RectTransform contentPanel;

        [SerializeField]
        protected MouseFollower mouseFollower;

        [SerializeField]
        protected ItemActionPanel actionPanel;

        [SerializeField]
        protected TMP_Text descriptionPanel;

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

        protected int currentlyDraggedItemIndex = -1;

        protected bool ctrlPressed = false;

        public static bool CtrlPressed;
        public static HashSet<KeyCode> currentlyPressedKeys = new HashSet<KeyCode>();

        public event Action<int> OnSelect, OnUnselect, OnSplit, OnDrop,
                OnItemActionRequested,
                OnStartDragging;

        public event Action<int, int> OnSwapItems;

        private void Awake()
        {
            Hide();
            mouseFollower.Toggle(false);
        }

        public void InitializeInventoryUI(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIInventoryItem uiItem =
                    Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel, false);
                listOfUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        internal void ResetAllItems()
        {
            foreach (var item in listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            DeselectAllItems();
            listOfUIItems[itemIndex].Select();
            descriptionPanel.text = description;
        }

        public void UpdateData(int itemIndex, InventoryItem item)
        {
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(item);
            }
        }

        protected virtual void HandleShowItemActions(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        protected virtual void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            if (
                !RectTransformUtility.RectangleContainsScreenPoint(
                contentPanel, 
                Input.mousePosition, 
                Camera.main)
            ) {
                OnDrop?.Invoke(listOfUIItems.IndexOf(inventoryItemUI));
            }
            ResetDraggedItem();
        }

        protected virtual void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
        }

        protected virtual void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        protected virtual void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            if (CtrlPressed) {
                OnSplit?.Invoke(index);
            }
            currentlyDraggedItemIndex = index;
            OnStartDragging?.Invoke(index);
        }

        public virtual void CreateDraggedItem(InventoryItem itemData)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(itemData);
        }

        protected virtual void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            if (inventoryItemUI.selected) {
                OnUnselect?.Invoke(index);
            } else {
                OnSelect?.Invoke(index);
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
            DeselectAllItems();
        }

        public void AddAction(string actionName, Action performAction)
        {
            actionPanel.AddButon(actionName, performAction);
        }

        public void ShowItemAction(int itemIndex)
        {
            actionPanel.Toggle(true);
            actionPanel.transform.position = listOfUIItems[itemIndex].transform.position;
        }

        public void HideItemAction()
        {
            actionPanel.Toggle(false);
        }

        public void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listOfUIItems)
            {
                item.Deselect();
            }
            actionPanel.Toggle(false);
        }

        public void DeselectItem(int index)
        {
            OnUnselect?.Invoke(index);
        }

        public void Hide()
        {
            actionPanel.Toggle(false);
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        public void Update() {
            ctrlPressed = Input.GetKeyDown(KeyCode.LeftControl);
        }

        protected void OnGUI()
        {
            if (!Event.current.isKey) return;

            if (Event.current.keyCode != KeyCode.None)
            {
                if (Event.current.type == EventType.KeyDown)
                {
                    currentlyPressedKeys.Add(Event.current.keyCode);
                }
                else if (Event.current.type == EventType.KeyUp)
                {
                    currentlyPressedKeys.Remove(Event.current.keyCode);
                }
            }

            CtrlPressed = Event.current.control;
        }
    }
}