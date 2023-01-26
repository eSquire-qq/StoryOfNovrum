using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Inventory.Interaction;
using System;
using System.Timers;

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

    protected GameObject runAwayTarget;

    public void Start()
    {
        interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
        detectionnArea = GetComponentInChildren(typeof(DetectionArea)) as DetectionArea;
        detectionnArea.OnAreaStay += OnDetectionRadiusStay;
        detectionnArea.OnAreaExit += OnDetectionRadiusExit;
    }

    protected void OnDetectionRadiusStay(Collider2D collision)
    {
        if (runAwayTarget)
            return;
        if (collision.gameObject.tag == "Player") {
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
        if (runAwayTarget)
            return;
        aiPath.maxSpeed = 1f;
        GameObject currentInteractionItem = interactionArea.GetCurrentItem();
        if (currentInteractionItem && GameObject.ReferenceEquals(target, currentInteractionItem)) {
            OnInteraction?.Invoke(new object());

            runAwayTarget = new GameObject("GhostRunAwayTarget");
            runAwayTarget.transform.position = (target.transform.position - (transform.position/2));
            target = runAwayTarget;
            aiPath.maxSpeed = 2f;
            aiPath.SearchPath();

            GameObject.Destroy(runAwayTarget.gameObject, 2f);
        }
    }

    public void FixedUpdate()
    {
        if (target == null) {
            return;
        }

        if (aiPath.steeringTarget != null) {
            interactionArea.transform.position = Utils.PositionBetween(transform.position, aiPath.steeringTarget, 0.75f);
        }

        RaycastHit2D targetHit = Physics2D.Linecast(transform.position, target.transform.position, 
        ((1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("MiddleLayer"))));

        if (targetHit.collider?.tag == "Player")
        {
            aiDestination.target = target.transform;
            aiPath.SearchPath();
        }
    }
}
