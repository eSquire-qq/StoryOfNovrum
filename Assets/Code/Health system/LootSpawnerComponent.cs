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
                // Створюється об'єкт випадаючого предмету
                GameObject dropItem = Instantiate(itemPrefab) as GameObject;
                // Створений об'єкт бере компонент підіймаючого предмету
                // встановлюється що це за предмет, та його кількість
                dropItem.GetComponent<PickableItemObject>().InventoryItem = item.item;
                dropItem.GetComponent<PickableItemObject>().Quantity = item.quantity;
                // Встановлюється позиція цього предмету
                dropItem.transform.position = transform.position;
                droppedItems.Add(dropItem);
            }
        }
    }

    public void FixedUpdate()
    {
        // Якщо випадіння предмету не завершено та кількість предмету більше за 0
        if (!itemsDropFinished && droppedItems.Count > 0) {
            foreach(GameObject item in droppedItems)
            {
                // Даємо фізичну поведінку випадаючому предмету
                Rigidbody2D dropItemRb = item.GetComponent<Rigidbody2D>() as Rigidbody2D;
                // Якщо стоврена фізична поведінка
                if (dropItemRb) {
                    //Синхронізація за допомогою базового перетворення ігрового об’єкта
                    Physics2D.SyncTransforms();
                    // Встановлюємо радіус в якому випаде предмет
                    float dropRadius = (float)((float)droppedItems.Count * 0.1);
                    // Передвигаємо предмет на позицію в попередньому радіусі
                    dropItemRb.MovePosition(
                        item.transform.position + new Vector3(UnityEngine.Random.Range(-dropRadius, dropRadius), UnityEngine.Random.Range(-dropRadius, dropRadius)));
                }
            }
            itemsDropFinished = true;
        }
    }
}

// Задаються параметри об'єкту предмета
[Serializable]
public struct LootItem
{
    public ItemSO item;
    public float dropChance;
    public int quantity;
}

