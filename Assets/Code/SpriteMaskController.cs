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

    //List of objects that we are colliding with
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
                //check if the object is on the same layer and is in front of the player sprite

                if (
                    playerSpriteRenderer.sortingLayerName == renderer.sortingLayerName
                    && playerSpriteRenderer.sortingOrder <= renderer.sortingOrder
                    //check the Y sorting order
                    && playerSpriteRenderer.transform.position.y > renderer.transform.position.y) 
                {
                    //if yes enable the sprite mask
                    spriteMask.enabled = true;
                    playerSpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                    return;
                }
                else
                {
                    //else disable the sprite mask
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
