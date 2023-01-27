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
    public GameObject target; 
    protected InteractionArea interactionArea;
    protected DetectionArea detectionnArea;

    public event Action<object> OnInteraction;

    protected GameObject runAwayTarget;

    public Animator animator;
    protected bool isRunning;
    private Rigidbody2D rb;

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
        AIAnimation();

        if (runAwayTarget)
            return;
        aiPath.maxSpeed = 1f;
        List<GameObject> currentInteractionItems = interactionArea.GetCurrentItems();
        GameObject friend = currentInteractionItems.Find(x => x.tag == "Enemy") as GameObject;
        if (friend != null && !GameObject.ReferenceEquals(friend, gameObject))
        {
            target = friend;
            RunAway();
            return;
        }

        if (currentInteractionItems.Contains(target)) {
            animator.SetTrigger("Attack");
        }
    }


    public void DoDamageToTarget()
    {
        OnInteraction?.Invoke(new object());
        RunAway();
    }

    protected void RunAway()
    {
        if (runAwayTarget == null) {
            runAwayTarget = new GameObject("GhostRunAwayTarget");
            runAwayTarget.transform.position = (target.transform.position - (transform.position/2));
        }
        target = runAwayTarget;
        aiPath.maxSpeed = 2f;
        aiPath.SearchPath();

        GameObject.Destroy(runAwayTarget.gameObject, 2f);
    }

    protected void AIAnimation()
    {
        Vector3 steeringVector = Vector3.Normalize(aiPath.steeringTarget - transform.position);

        if (aiPath.TargetReached)
        {
            if(isRunning)
            {
                isRunning = false;
                animator.SetBool("isRunning", isRunning);
            }
            return;
        }

        if(steeringVector.x != 0 || steeringVector.y != 0)
        {
            animator.SetFloat("Horizontal", steeringVector.x);
            animator.SetFloat("Vertical", steeringVector.y);    
            if(!isRunning)
            {
                isRunning = true;
                animator.SetBool("isRunning", isRunning);
            }
        }
    }

    public void FixedUpdate()
    {
        if (target == null) {
            return;
        }

        if (aiPath.steeringTarget != null) {
            interactionArea.transform.position = Utils.PositionBetween(transform.position, aiPath.steeringTarget, 0.8f);
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
