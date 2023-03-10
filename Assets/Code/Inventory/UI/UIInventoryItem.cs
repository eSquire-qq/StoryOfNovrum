using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private Image quantityBg;
        [SerializeField]
        private TMP_Text quantityTxt;

        [SerializeField]
        private Image selectionImage;

        public event Action<UIInventoryItem> OnItemClicked,
            OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag,
            OnRightMouseBtnClick;

        private bool empty = true;
        public bool selected = false;

        public void Awake()
        {
            ResetData();
            Deselect();
        }
        public void ResetData()
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
        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantityTxt.text = quantity + "";
            quantityBg.enabled = quantity > 1;
            quantityTxt.enabled = quantity > 1;
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