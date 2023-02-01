using Inventory;
using Inventory.Interaction;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Timers;
using Animations;
using System.Linq;

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
    protected AnimatorController animatorController;

    [SerializeField]
    protected Rigidbody2D rb;

    protected bool attackCoolDown = false;
    public void Awake()
    {
        interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
        wieldObjectController = GetComponentInChildren(typeof(WieldObjectController)) as WieldObjectController;
        animatorController = GetComponent(typeof(AnimatorController)) as AnimatorController;
        rb = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    }

	public void Attack()
    {
		if (attackCoolDown)
		{
            return;
		}

        float cooldown = this.cooldown;
        string animation = "Attack";
        if (wieldObjectController) {
            InventoryItem currentWeapon = wieldObjectController.wieldItem;
            if (currentWeapon.item != null)
            {
                float weaponCooldown = currentWeapon.itemState.Find(x => x.itemParameter.ParameterName == GlobalConstants.Parameters.COOLDOWN).value;
                if (weaponCooldown > 0) {
                    cooldown = weaponCooldown;
                }
                List<string> weaponAnimations = currentWeapon.item.animations.Where(x => x.type.AnimationType == GlobalConstants.Animations.ATTACK).Select(x => x.name).ToList<string>();
                if (weaponAnimations.Count() > 0) {
                    animation = weaponAnimations[UnityEngine.Random.Range(0, weaponAnimations.Count())];
                }
            }
        }

        if (animatorController)
        {
            animatorController.ChangeAnimationState(animation, true);
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
                damage = currentWeapon.itemState.Find(x => x.itemParameter.ParameterName == GlobalConstants.Parameters.DAMAGE).value;
                float weaponKnockBackMultiplier = currentWeapon.itemState.Find(x => x.itemParameter.ParameterName == GlobalConstants.Parameters.KNOCKBACK).value;
                if (weaponKnockBackMultiplier > 0) {
                    knockBackMultiplier = weaponKnockBackMultiplier;
                }
                int index = currentWeapon.itemState.FindIndex(x => x.itemParameter.ParameterName == GlobalConstants.Parameters.DURABILITY);
                if (index >= 0) {
                    ItemParameter durability = new ItemParameter();
                    durability.itemParameter = currentWeapon.itemState[index].itemParameter;
                    durability.value = currentWeapon.itemState[index].value - 1;
                    currentWeapon.itemState[index] = durability;
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
