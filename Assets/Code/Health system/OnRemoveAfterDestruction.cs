using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRemoveAfterDestruction : AOnDestruction
{
    [SerializeField]
    float timer;

    protected override void OnDestruction()
    {
        Destroy(gameObject, timer);
    }
}
