using Inventory;
using Inventory.Interaction;
using Inventory.Model;
using Inverntory.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    protected InteractionArea interactionArea;
    protected WieldObjectController wieldObjectController;

    [SerializeField]
    protected IInteractionInvoker invoker;

    private GameObject attackArea = default;

    private bool attacking = false;

    private float timeToAttack = 0.25f;
    private float timer = 0f;

    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
    }

    public void Awake()
    {
        interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
        wieldObjectController = GetComponentInChildren(typeof(WieldObjectController)) as WieldObjectController;
        invoker = GetComponent(typeof(IInteractionInvoker)) as IInteractionInvoker;
        invoker.OnInteraction += Attack;
    }

    public void Attack(object interactionContext)
	{
        Health attackObject = interactionArea.GetCurrentItem()?.GetComponent<Health>();
        if (attackObject == null)
		{
            return;
		}
        float damage = 1f;
        InventoryItem currentWeapon = wieldObjectController.wieldItem;
        if (currentWeapon.item != null)
		{
            damage = currentWeapon.itemState.Find(x => x.itemParameter.ParameterName == "Damage").value;
        }
        attackObject.TakeDamage(damage);
	}

}
