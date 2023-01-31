using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnObjectDestruction : AOnDestruction
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    Collider2D collider;

    protected void Start()
    {
        base.Start();
        animator = GetComponent(typeof(Animator)) as Animator;
        collider = GetComponent(typeof(Collider2D)) as Collider2D;
    }

    protected override void OnDestruction()
    {
        if (collider) {
            collider.enabled = false;
        }
        if (animator) {
            animator.SetTrigger(GlobalConstants.Triggers.ONEDESTRUCTION);
        }
    }
}
