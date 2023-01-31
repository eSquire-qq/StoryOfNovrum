using Inventory;
using Inventory.Interaction;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Timers;

public class SimpleMeleeAttackComponent : MonoBehaviour
{
    protected InteractionArea interactionArea;
    protected WieldObjectController wieldObjectController;

    [SerializeField]
    protected float damage;

    [SerializeField]
    protected float cooldown;

    [SerializeField]
    protected float knockBackMultiplier;

    [SerializeField]
    protected Animator animator;

    [SerializeField]
    protected Rigidbody2D rb;

    protected bool attackCoolDown = false;
    public void Awake()
    {
        interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
        wieldObjectController = GetComponentInChildren(typeof(WieldObjectController)) as WieldObjectController;
        animator = GetComponent(typeof(Animator)) as Animator;
        rb = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    }

	public void Attack()
    {
		if (attackCoolDown)
		{
            return;
		}

        if (animator)
        {
            animator.SetTrigger(GlobalConstants.Triggers.ATTACK);
        }

        float cooldown = this.cooldown;
        if (wieldObjectController) {
            InventoryItem currentWeapon = wieldObjectController.wieldItem;
            if (currentWeapon.item != null)
            {
                float weaponCooldown = currentWeapon.itemState.Find(x => x.itemParameter.ParameterName == GlobalConstants.Parameters.COOLDOWN).value;
                if (weaponCooldown > 0) {
                    cooldown = weaponCooldown;
                }
            }
        }

        if (cooldown > 0) {
            attackCoolDown = true;
            Timer attackCoolDownTimer = new Timer(cooldown);
		    attackCoolDownTimer.Elapsed += OnAttackCooldownTimerPassed;
            attackCoolDownTimer.AutoReset = false;
            attackCoolDownTimer.Enabled = true;
        }
    }

    public void DoDamage()
    {
        Health attackObject = interactionArea.GetCurrentItems()?.Find(x => (x.GetComponent<Health>() != null && x != gameObject))?.GetComponent<Health>();
        if (attackObject == null)
        {
            return;
        }
        float damage = this.damage;
        float knockBackMultiplier = this.knockBackMultiplier;
        if (wieldObjectController) {
            InventoryItem currentWeapon = wieldObjectController.wieldItem;
            if (currentWeapon.item != null)
            {
                damage = currentWeapon.itemState.Find(x => x.itemParameter.ParameterName == GlobalConstants.Triggers.DAMAGE).value;
                float weaponKnockBackMultiplier = currentWeapon.itemState.Find(x => x.itemParameter.ParameterName == "KnockBack").value;
                if (weaponKnockBackMultiplier > 0) {
                    knockBackMultiplier = weaponKnockBackMultiplier;
                }
            }
        }
        Vector2 damageVector = (attackObject.transform.position - transform.position);
        damageVector *= ((rb != null && rb.mass > 1) ? rb.mass : 1) * damage * knockBackMultiplier;
        attackObject.TakeDamage(damage, damageVector);
    }

    protected void OnAttackCooldownTimerPassed(object source, ElapsedEventArgs e)
    {
        attackCoolDown = false;
    }

}
