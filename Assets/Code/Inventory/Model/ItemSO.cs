using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Actions;

namespace Inventory.Model
{
    public abstract class ItemSO : ScriptableObject
    {
        [field: SerializeField]
        public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        [field: SerializeField]
        public List<ItemParameter> DefaultParametersList { get; set; }

        [field: SerializeField]
        public List<ActionData> actionDatas { get; set; }
        
        public List<string> actionNames { get; protected set;}

        [field: SerializeField]
        public List<ItemAnimation> animations { get; set; }

    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSO itemParameter;
        public float value;

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }

    [Serializable]
    public class ItemAnimation
    {
        [field: SerializeField]
        public AnimationTypeSO type  {get; protected set;}

        [field: SerializeField]
        public string name {get; protected set;}
    }

    [Serializable]
    public class ActionData
    {
        [field: SerializeField]
        public ItemActionSO action;

        [field: SerializeField]
        public string actionName { get; protected set;}
    }
}

