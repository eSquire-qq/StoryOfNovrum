using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public void SetMaxHealth(float health)
	{
        // Встановлення індикатор ввідповідно до максимального
        slider.maxValue = health;
        slider.value = health;
        
        // Колір індикатора змінюється залежно від
        // кількості здорвя
        fill.color = gradient.Evaluate(1f);
	}

    public void SetHealth(float health, float maxHealth)
	{
        slider.gameObject.SetActive(health < maxHealth);
        // Положення індикатора відповідає кількості здоров'я
        slider.maxValue = maxHealth;
        slider.value = health;
        // Колір індикатора відповідає кількості здорв'я
        fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
