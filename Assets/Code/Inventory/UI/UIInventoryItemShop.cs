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
    public class UIInventoryItemShop : UIInventoryItem
    {


        public override void SetData(InventoryItem item)
        {
            int cost = Convert.ToInt32(item.itemState.Find(x => x.itemParameter.ParameterName == "Cost").value);
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = item.item.ItemImage;
            quantityTxt.text = cost + "";
            quantityBg.enabled = cost > 1;
            quantityTxt.enabled = cost > 1;
            empty = false;
        }
    }
}