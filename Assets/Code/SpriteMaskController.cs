using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SpriteMaskController : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer playerSpriteRenderer;

    [SerializeField]
    protected SpriteMask spriteMask;

    protected Collider2D spriteMaskCollider;

    // Список об'єктів, з якими ми стикаємося
    protected List<SpriteRenderer> otherRendereres = new List<SpriteRenderer>();

    protected bool checking = false;

    public void Awake()
    {
        spriteMaskCollider = GetComponent<Collider2D>();
        spriteMaskCollider.isTrigger = true;
    }

    public void Update()
    {
        if (checking)
        {
            foreach (SpriteRenderer renderer in otherRendereres)
            {
                // Перевірити, чи знаходиться об’єкт на тому самому шарі та перед спрайтом гравця

                if (
                    playerSpriteRenderer.sortingLayerName == renderer.sortingLayerName
                    && playerSpriteRenderer.sortingOrder <= renderer.sortingOrder
                    // Перевірте порядок сортування Y
                    && playerSpriteRenderer.transform.position.y > renderer.transform.position.y) 
                {
                    // Якщо так, увімкніть маску спрайту
                    spriteMask.enabled = true;
                    playerSpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                    return;
                }
                else
                {
                    // Інакше вимкніть маску спрайту
                    spriteMask.enabled = false;
                    playerSpriteRenderer.maskInteraction = SpriteMaskInteraction.None;
                }
                
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != GlobalConstants.Tags.MASKTRIGGER)
            return;
        SpriteRenderer spriteRenderer = collision.GetComponentInParent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            otherRendereres.Add(spriteRenderer);
            checking = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != GlobalConstants.Tags.MASKTRIGGER)
            return;
        SpriteRenderer spriteRenderer = collision.GetComponentInParent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            otherRendereres.Remove(spriteRenderer);
            if(otherRendereres.Count <= 0)
            {
                checking = false;
                spriteMask.enabled = false;
                playerSpriteRenderer.maskInteraction = SpriteMaskInteraction.None;
            }
                
        }
    }

}
