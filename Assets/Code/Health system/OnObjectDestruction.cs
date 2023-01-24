using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnObjectDestruction : AOnDestruction
{
    protected override void OnDestruction()
    {
        Debug.Log("Aboba");
    }
}
