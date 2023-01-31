using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Inventory.Interaction;
using System;
using System.Timers;

public class EnemyGhostController : MonoBehaviour
{
    [SerializeField]
    protected AIDestinationSetter aiDestination;
    [SerializeField]
    protected AIPath aiPath;
    public GameObject target; 
    protected InteractionArea interactionArea;
    protected DetectionArea detectionnArea;

    protected SimpleMeleeAttackComponent attackComponent;

    protected GameObject runAwayTarget;

    public Animator animator;
    protected bool isRunning;
    private Rigidbody2D rb;

    public void Start()
    {
        interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
        attackComponent = GetComponentInChildren(typeof(SimpleMeleeAttackComponent)) as SimpleMeleeAttackComponent;
        detectionnArea = GetComponentInChildren(typeof(DetectionArea)) as DetectionArea;
        detectionnArea.OnAreaStay += OnDetectionRadiusStay;
        detectionnArea.OnAreaExit += OnDetectionRadiusExit;
    }

    protected void OnDetectionRadiusStay(Collider2D collision)
    {
        if (runAwayTarget)
            return;
        if (collision.gameObject.tag == GlobalConstants.Tags.PLAYER) {
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
        GameObject friend = currentInteractionItems.Find(x => x.tag == GlobalConstants.Tags.ENEMY) as GameObject;
        if (friend != null && !GameObject.ReferenceEquals(friend, gameObject))
        {
            target = friend;
            RunAway();
            return;
        }

        if (currentInteractionItems.Contains(target)) {
            attackComponent?.Attack();
        }
    }

    public void RunAway()
    {
        if (runAwayTarget == null) {
            runAwayTarget = new GameObject("GhostRunAwayTarget");
            if (target) {
                runAwayTarget.transform.position = (target.transform.position - (transform.position/2));
            } else {
                runAwayTarget.transform.position = new Vector3(
                    transform.position.x + UnityEngine.Random.Range(-5f, 5f),
                    transform.position.y + UnityEngine.Random.Range(-5f, 5f), 0f);
            }
        }
        target = runAwayTarget;
        aiPath.maxSpeed = 1.5f;
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
            interactionArea.transform.position = transform.position + steeringVector/2;
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

        RaycastHit2D targetHit = Physics2D.Linecast(transform.position, target.transform.position, 
        ((1 << LayerMask.NameToLayer(GlobalConstants.Layers.DEFAULT)) | (1 << LayerMask.NameToLayer(GlobalConstants.Layers.MIDDLELAYER))));

        if (targetHit.collider?.tag == GlobalConstants.Tags.PLAYER)
        {
            aiDestination.target = target.transform;
            aiPath.SearchPath();
        }
    }
}
