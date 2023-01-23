using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

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

    public void TakeDamage(int damage)
	{
        currentHealth -= damage;
	}

}
