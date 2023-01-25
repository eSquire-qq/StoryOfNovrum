using Inventory;
using Inventory.Interaction;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Timers;

public class SimpleMeleeAttack : MonoBehaviour
{
    protected InteractionArea interactionArea;
    protected WieldObjectController wieldObjectController;

    [SerializeField]
    protected IInteractionInvoker<object> invoker;

    protected bool attackCoolDown = false;
    public void Awake()
    {
        interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
        wieldObjectController = GetComponentInChildren(typeof(WieldObjectController)) as WieldObjectController;
        invoker = GetComponent(typeof(IInteractionInvoker<object>)) as IInteractionInvoker<object>;
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
        float damage = 5f;
        float cooldown = 500f;
        if (wieldObjectController) {
            InventoryItem currentWeapon = wieldObjectController.wieldItem;
            if (currentWeapon.item != null)
            {
                damage = currentWeapon.itemState.Find(x => x.itemParameter.ParameterName == "Damage").value;
            }
        }
        attackObject.TakeDamage(damage);
        attackCoolDown = true;

        Timer attackCoolDownTimer = new Timer(cooldown);
		attackCoolDownTimer.Elapsed += OnAttackCooldownTimerPassed;
        attackCoolDownTimer.AutoReset = false;
        attackCoolDownTimer.Enabled = true;
    }

    protected void OnAttackCooldownTimerPassed(object source, ElapsedEventArgs e)
    {
        attackCoolDown = false;
    }

}