using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRemoveAfterDestruction : AOnDestruction
{
    [SerializeField]
    float timer;

    // Знищення предмету як ігрового об'єкту
    protected override void OnDestruction()
    {
        Destroy(gameObject, timer);
    }
}
