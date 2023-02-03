using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Inventory.Interaction;
using System;
using System.Timers;
using Animations;
using System.Linq;

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
    protected bool isMoving;
    private Rigidbody2D rb;

    [SerializeField]
    protected AnimatorController animatorController;

    [SerializeField]
	protected List<AudioClip> runSounds;

	[SerializeField]
	protected AudioSource audioSourceRunSound;

    [SerializeField]
    protected AudioClip combatMusic;

    public void Start()
    {
        interactionArea = GetComponentInChildren(typeof(InteractionArea)) as InteractionArea;
        attackComponent = GetComponentInChildren(typeof(SimpleMeleeAttackComponent)) as SimpleMeleeAttackComponent;
        detectionnArea = GetComponentInChildren(typeof(DetectionArea)) as DetectionArea;
        animatorController = GetComponent(typeof(AnimatorController)) as AnimatorController;
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

        transform.localScale = new Vector2(steeringVector.x < 0 ? -1f : 1f, 1f);

        if(steeringVector.x != 0 || steeringVector.y != 0)
        {
            if (audioSourceRunSound && !audioSourceRunSound.isPlaying && runSounds.Count() > 0)
			 	audioSourceRunSound.PlayOneShot(runSounds[UnityEngine.Random.Range(0, runSounds.Count())]);
            if (target != null) {
                interactionArea.transform.position = transform.position + Vector3.Normalize(target.transform.position - transform.position)/2;
            } else {
                interactionArea.transform.position = transform.position + steeringVector/2;
            }
            animatorController.ChangeAnimationState(GlobalConstants.Animations.WALK, animatorController.currentAnimationState == GlobalConstants.Animations.IDLE ? true : false);
        }
        if (aiPath.TargetReached)
        {
            animatorController.ChangeAnimationState(GlobalConstants.Animations.IDLE, animatorController.currentAnimationState == GlobalConstants.Animations.WALK ? true : false);
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
            if (EventManager.instance) EventManager.TriggerEvent("EnterCombat", new Dictionary<string, object>(){["combatMusic"] = combatMusic});
            aiDestination.target = target.transform;
            aiPath.SearchPath();
        }
    }
}
