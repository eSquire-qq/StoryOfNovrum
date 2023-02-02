using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class AnimationTypeSO : ScriptableObject
    {
        [field: SerializeField]
        public string AnimationType { get; private set; }
    }
}