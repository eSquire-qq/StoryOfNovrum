using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToObject : MonoBehaviour
{
    [SerializeField]
    protected GameObject target;

    public void FixedUpdate()
    {
        if (target == null)
            return;
        transform.position = target.transform.position;
    }
}
