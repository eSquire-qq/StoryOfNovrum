using Inventory;
using Inventory.Interaction;
using Inventory.Model;
using Inverntory.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Timers;

public class PlayerAttack : MonoBehaviour
{
    protected InteractionArea interactionArea;
    protected WieldObjectController wieldObjectController;

    [SerializeField]
    protected IInteractionInvoker invoker;

    private GameObject attackArea = default;

    protected bool attackCoolDown = false;
    protected float attackCoolDownTime = 0.5f;

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
		if (attackCoolDown)
		{
            return;
		}

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
        attackCoolDown = true;

        Timer attackCoolDownTimer = new Timer(500);
		attackCoolDownTimer.Elapsed += OnAttackCooldownTimerPassed;
        attackCoolDownTimer.AutoReset = false;
        attackCoolDownTimer.Enabled = true;
    }

    protected void OnAttackCooldownTimerPassed(object source, ElapsedEventArgs e)
    {
        attackCoolDown = false;
    }

}