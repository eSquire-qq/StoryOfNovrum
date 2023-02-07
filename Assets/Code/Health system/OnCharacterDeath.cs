using System.Collections;
using System.Collections.Generic;
using Animations;
using UnityEngine;

public class OnCharacterDeath : OnObjectDestruction
{
    protected override void OnDestruction()
    {
        base.OnDestruction();
        Component[] components = GetComponents(typeof(Component));
 
        foreach(Component component in components){
            if( !(component.GetType() == typeof(Animator)
             || component.GetType() == typeof(SpriteRenderer) 
             || component.GetType() == typeof(AnimatorController)
             || component.GetType() == typeof(Transform))){
                Destroy(component);
            }
        }
    }
}
