using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Inventory.Interaction;
using System;

public class EnemyGhostController : MonoBehaviour, IInteractionInvoker<object>
{
    [SerializeField]
    protected AIDestinationSetter aiDestination;
    [SerializeField]
    protected AIPath aiPath;
    protected GameObject target; 
    protected InteractionArea interactionArea;
    protected DetectionArea detectionnArea;

    public event Action<object> OnInteraction;

    protected bool attackCoolDown = false;

    public void Start()
    {
        interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
        detectionnArea = GetComponentInChildren(typeof(DetectionArea)) as DetectionArea;
        detectionnArea.OnAreaEnter += OnDetectionRadiusEnter;
        detectionnArea.OnAreaExit += OnDetectionRadiusExit;
    }

    protected void OnDetectionRadiusEnter(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") {
            target = collision.gameObject;
        }
    }

    protected void OnDetectionRadiusExit(Collider2D collision)
    {
        if (target == collision.gameObject) {
            target = null;
            aiDestination.target = null;
        }
    }

    public void Update()
    {
        GameObject currentInteractionItem = interactionArea.GetCurrentItem();
        if (currentInteractionItem && GameObject.ReferenceEquals(target, currentInteractionItem)) {
            OnInteraction?.Invoke(new object());
        }
    }

    public void FixedUpdate()
    {
        if (target == null) {
            return;
        }
        Debug.DrawRay(transform.position, Vector3.Normalize(target.transform.position - transform.position), Color.yellow);
        RaycastHit2D targetHit = Physics2D.Linecast(transform.position, target.transform.position, 
        ((1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("MiddleLayer"))));

        if (targetHit.collider) {
            Debug.DrawRay(transform.position, (targetHit.transform.position - transform.position), Color.red);
        }

        if (targetHit.collider?.tag == "Player")
        {
            aiDestination.target = target.transform;
        }
    }
}
