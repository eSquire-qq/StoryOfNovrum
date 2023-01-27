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

    [SerializeField]
    protected Animator animator;

    protected bool attackCoolDown = false;
    public void Awake()
    {
        interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
        wieldObjectController = GetComponentInChildren(typeof(WieldObjectController)) as WieldObjectController;
        invoker = GetComponent(typeof(IInteractionInvoker<object>)) as IInteractionInvoker<object>;
        animator = GetComponent(typeof(Animator)) as Animator;
        invoker.OnInteraction += Attack;
    }

	public void Attack(object interactionContext)
    {
		if (attackCoolDown)
		{
            return;
		}

        if (animator)
        {
            animator.SetTrigger("Attack");
        }

        float cooldown = 500f;
        if (wieldObjectController) {
            InventoryItem currentWeapon = wieldObjectController.wieldItem;
            if (currentWeapon.item != null)
            {
                cooldown = currentWeapon.itemState.Find(x => x.itemParameter.ParameterName == "Cooldown").value;
            }
        }
        attackCoolDown = true;

        Timer attackCoolDownTimer = new Timer(cooldown);
		attackCoolDownTimer.Elapsed += OnAttackCooldownTimerPassed;
        attackCoolDownTimer.AutoReset = false;
        attackCoolDownTimer.Enabled = true;
    }

    public void DoDamage()
    {
        Health attackObject = interactionArea.GetCurrentItems()?.Find(x => (x.GetComponent<Health>() != null && x != gameObject))?.GetComponent<Health>();
        if (attackObject == null)
        {
            return;
        }
        float damage = 5f;
        if (wieldObjectController) {
            InventoryItem currentWeapon = wieldObjectController.wieldItem;
            if (currentWeapon.item != null)
            {
                damage = currentWeapon.itemState.Find(x => x.itemParameter.ParameterName == "Damage").value;
            }
        }
        attackObject.TakeDamage(damage);
    }

    protected void OnAttackCooldownTimerPassed(object source, ElapsedEventArgs e)
    {
        attackCoolDown = false;
    }

}