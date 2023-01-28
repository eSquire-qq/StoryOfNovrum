using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    public HealthBarScript healthBar;

    protected Animator animator; 
    public event Action OnNoHealth;
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar) {
            healthBar.SetMaxHealth(maxHealth);
        }
        animator = GetComponent(typeof(Animator)) as Animator;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
		{
            TakeDamage(20);
		}
        if (currentHealth <= 0)
		{
            OnNoHealth?.Invoke();
		}
        if (healthBar) {
            healthBar.SetHealth(currentHealth);
        }
    }

    public void TakeDamage(float damage)
	{
        currentHealth -= damage;
        if (animator) {
            animator.SetTrigger("TakeDamage");
        }
	}

    public void Heal(float health)
	{
        currentHealth += health;
	}

}
