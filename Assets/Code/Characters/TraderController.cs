using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Inventory.Interaction;
using System;
using System.Timers;
using Animations;
using System.Linq;
using Inventory.UI;
using Inventory;

public class TraderController : MonoBehaviour
{
    [SerializeField]
    protected DetectionArea detectionnArea;
    private Rigidbody2D rb;

    [SerializeField]
    protected AnimatorController animatorController;

    [SerializeField]
    protected UIInventoryPage inventoryUIController;

    [SerializeField]
    protected ShopInventoryController inventoryController;

    [SerializeField]
    protected InteractiveObject inverativeObjectComponent;

    [SerializeField]
    public GameObject player;

    public void Start()
    {
        detectionnArea = GetComponentInChildren(typeof(DetectionArea)) as DetectionArea;
        animatorController = GetComponent(typeof(AnimatorController)) as AnimatorController;
        inverativeObjectComponent = GetComponent(typeof(InteractiveObject)) as InteractiveObject;
        detectionnArea.OnAreaStay += OnDetectionRadiusStay;
        detectionnArea.OnAreaExit += OnDetectionRadiusExit;
        inverativeObjectComponent.OnInteraction += OnInteraction;
        if (inventoryUIController != null) {
            inventoryUIController.Hide();
        }
    }

    protected void OnDetectionRadiusStay(Collider2D collision)
    {
        if (collision.gameObject.tag == GlobalConstants.Tags.PLAYER) {
            player = collision.gameObject;
        } else {
            player = null;
        }
        Debug.Log(collision.gameObject.tag);
    }

    protected void OnInteraction(object context)
    {
        if (inventoryUIController.gameObject.activeSelf) {
            inventoryUIController.Hide();
            return;
        }
        inventoryUIController.Show();
    }

    protected void OnDetectionRadiusExit(Collider2D collision)
    {
        if (collision.gameObject.tag == GlobalConstants.Tags.PLAYER && inventoryUIController != null) {
            inventoryUIController.Hide();
        }
    }

}
