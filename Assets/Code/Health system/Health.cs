using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animations;
using System.Linq;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public bool dead;

    public HealthBarScript healthBar;

    protected Rigidbody2D rb;
    protected AnimatorController animatorController; 
    public event Action OnNoHealth;
    protected Vector3 knockBack;

    [SerializeField]
	protected List<AudioClip> hurtSounds;

    [SerializeField]
    protected AudioSource audioSourceHurtSound;

    void Start()
    {
        // На початку гри поточне здоров'я дорівнює
        // максимальному здоров'ю
        currentHealth = maxHealth;
        dead = false;
        // Індикатор здоров'я відповідає максимальному
        // здоров'ю
        if (healthBar) {
            healthBar.SetMaxHealth(maxHealth);
        }
        animatorController = GetComponent(typeof(AnimatorController)) as AnimatorController;
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
            healthBar.SetHealth(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(float damage, Vector3 knockBack)
	{
        // Поточне здоров'я зменшується на 
        // кількість ориманого урону
        currentHealth -= damage;
        // Програвання звуку під час отримання урону 
        if (audioSourceHurtSound && hurtSounds.Count() > 0)
			 	audioSourceHurtSound.PlayOneShot(hurtSounds[UnityEngine.Random.Range(0, hurtSounds.Count())]);
        // Відігравання анімації отримання урону       
        if (animatorController) {
            animatorController.ChangeAnimationState(GlobalConstants.Animations.TAKEDAMAGE, true);
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
        // Відновлює поточне здоров'я
        currentHealth += health;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
	}

}
