using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using Inventory.Model;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField]
        protected Image itemImage;

        [SerializeField]
        protected Image quantityBg;
        
        [SerializeField]
        protected TMP_Text quantityTxt;

        [SerializeField]
        protected Image selectionImage;

        public event Action<UIInventoryItem> OnItemClicked,
            OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag,
            OnRightMouseBtnClick;

        protected bool empty = true;
        public bool selected = false;

        public void Awake()
        {
            ResetData();
            Deselect();
        }
        public virtual void ResetData()
        {
            itemImage.gameObject.SetActive(false);
            quantityBg.enabled = false;
            quantityTxt.enabled = false;
            empty = true;
        }
        public void Deselect()
        {
            selectionImage.enabled = false;
            selected = false;
        }
        public virtual void SetData(InventoryItem item)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = item.item.ItemImage;
            quantityTxt.text = item.quantity + "";
            quantityBg.enabled = item.quantity > 1;
            quantityTxt.enabled = item.quantity > 1;
            empty = false;
        }

        public void Select()
        {
            selectionImage.enabled = true;
            selected = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty)
                return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }
    }
}