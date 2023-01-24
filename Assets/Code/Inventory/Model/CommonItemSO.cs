using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class CommonItemSO : ItemSO, IDestroyableItem, IItemWithActions
    {
        // [SerializeField]
        // private List<ModifierData> modifiersData = new List<ModifierData>();

        public void Awake()
        {
            actionNames = new List<string>();
            foreach (ActionData data in actionDatas)
            {
                actionNames.Add(data.actionName);
            }
        }

        public bool PerformAction(object actionTarget, string actionName)
        {
            ActionData action = actionDatas.Find(x => x.actionName.Equals(actionName));
            if (action == null) {
                return false;
            }
            action.action.PerformAction(actionTarget);
            return true;
        }
    }

    // [Serializable]
    // public class ModifierData
    // {
    //     public CharacterStatModifierSO statModifier;
    //     public float value;
    // }
}