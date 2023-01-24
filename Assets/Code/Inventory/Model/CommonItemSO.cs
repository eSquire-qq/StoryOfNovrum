using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Actions;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class CommonItemSO : ItemSO, IItemWithActions
    {

        public void Awake()
        {
            actionNames = new List<string>();
            foreach (ActionData data in actionDatas)
            {
                actionNames.Add(data.actionName);
            }
        }

        public bool PerformAction(ActionInput input, string actionName)
        {
            ActionData action = actionDatas.Find(x => x.actionName.Equals(actionName));
            if (action == null) {
                return false;
            }
            action.action.PerformAction(input);
            return true;
        }
    }

}