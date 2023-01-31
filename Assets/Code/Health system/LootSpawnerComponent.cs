using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using System;

public class LootSpawnerComponent : AOnDestruction
{
    [SerializeField]
    protected GameObject itemPrefab;

    [SerializeField]
    protected List<LootItem> possibleItems;

    [SerializeField]
    protected List<GameObject> droppedItems;

    protected bool itemsDropFinished;

    protected void Start()
    {
        base.Start();
        itemsDropFinished = false;
    }

    protected override void OnDestruction()
    {
        foreach(LootItem item in possibleItems)
        {
            if (UnityEngine.Random.Range(0, 100f) > (100 - item.dropChance)) {
                GameObject dropItem = Instantiate(itemPrefab) as GameObject;
                dropItem.GetComponent<PickableItemObject>().InventoryItem = item.item;
                dropItem.GetComponent<PickableItemObject>().Quantity = item.quantity;
                dropItem.transform.position = transform.position;
                droppedItems.Add(dropItem);
            }
        }
    }

    public void FixedUpdate()
    {
        if (!itemsDropFinished && droppedItems.Count > 0) {
            foreach(GameObject item in droppedItems)
            {
                Rigidbody2D dropItemRb = item.GetComponent<Rigidbody2D>() as Rigidbody2D;
                if (dropItemRb) {
                    Physics2D.SyncTransforms();
                    float dropRadius = (float)((float)droppedItems.Count * 0.1);
                    dropItemRb.MovePosition(
                        item.transform.position + new Vector3(UnityEngine.Random.Range(-dropRadius, dropRadius), UnityEngine.Random.Range(-dropRadius, dropRadius)));
                }
            }
            itemsDropFinished = true;
        }
    }
}

[Serializable]
public struct LootItem
{
    public ItemSO item;
    public float dropChance;

    public int quantity;
}

