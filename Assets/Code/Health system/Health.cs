using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    public HealthBarScript healthBar;

    public event Action OnNoHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(float damage)
	{
        currentHealth -= damage;
	}

    public void Heal(float health)
	{
        currentHealth += health;
	}

}
