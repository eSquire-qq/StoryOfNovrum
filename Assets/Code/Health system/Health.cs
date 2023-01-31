using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public bool dead;

    public HealthBarScript healthBar;

    protected Rigidbody2D rb;
    protected Animator animator; 
    public event Action OnNoHealth;
    protected Vector3 knockBack;
    void Start()
    {
        currentHealth = maxHealth;
        dead = false;
        if (healthBar) {
            healthBar.SetMaxHealth(maxHealth);
        }
        animator = GetComponent(typeof(Animator)) as Animator;
        rb = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    }

    void Update()
    {
        if (currentHealth <= 0 && dead == false)
		{
            dead = true;
            OnNoHealth?.Invoke();
		}
        if (currentHealth >= 0 && dead == true)
		{
            dead = false;
        }
        if (healthBar) {
            healthBar.SetHealth(currentHealth);
        }
    }

    public void TakeDamage(float damage, Vector3 knockBack)
	{
        currentHealth -= damage;
        if (animator) {
            animator.SetTrigger(GlobalConstants.Triggers.TAKEDAMAGE);
        }
        if (knockBack != null) {
            this.knockBack = (Vector3)knockBack;
        }
	}

    public void FixedUpdate()
    {
        if (knockBack.x != 0 || knockBack.y != 0) {
            rb.AddForce(knockBack, ForceMode2D.Force);
        }
        if (knockBack.x < 0) {
            knockBack.x += (0 - knockBack.x)/10;
        }
        if (knockBack.x > 0) {
            knockBack.x -= (0 + knockBack.x)/10;
        }
        if (knockBack.y < 0) {
            knockBack.y += (0 - knockBack.y)/10;
        }
        if (knockBack.y > 0) {
            knockBack.y -= (0 + knockBack.y)/10;
        }
    }

    public void Heal(float health)
	{
        currentHealth += health;
	}

}
