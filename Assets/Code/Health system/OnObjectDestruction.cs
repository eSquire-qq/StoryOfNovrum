using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animations;

public class OnObjectDestruction : AOnDestruction
{
    [SerializeField]
    protected AnimatorController animator;

    [SerializeField]
    protected Collider2D collider;

    protected void Start()
    {
        base.Start();
        animator = GetComponent(typeof(AnimatorController)) as AnimatorController;
        collider = GetComponent(typeof(Collider2D)) as Collider2D;
    }

    protected override void OnDestruction()
    {
        if (collider) {
            collider.enabled = false;
        }
        if (animator) {
            animator.ChangeAnimationState(GlobalConstants.Animations.ONEDESTRUCTION, true);
        }
    }
}
