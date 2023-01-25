using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGhostController : MonoBehaviour
{
    [SerializeField]
    protected AIDestinationSetter aiDestination;
    [SerializeField]
    protected AIPath aiPath;
    protected GameObject target; 

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") {
            target = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (target == collision.gameObject) {
            target = null;
            aiDestination.target = null;
        }
    }

    public void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector3.Normalize(target.transform.position - transform.position), Color.yellow);
        RaycastHit2D targetHit = Physics2D.Linecast(transform.position, target.transform.position, 
        ((1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("MiddleLayer"))));

        if (targetHit.collider) {
            Debug.DrawRay(transform.position, (targetHit.transform.position - transform.position), Color.red);
        }

        if (targetHit.collider?.tag == "Player")
        {
            Debug.Log(target.transform);
            aiDestination.target = target.transform;
        }
    }
}
