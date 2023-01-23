using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCharacterDeath : AOnDestruction
{
    protected override void OnDestruction()
    {
        Destroy(gameObject);
    }
}
