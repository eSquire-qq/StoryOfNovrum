using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AOnDestruction : MonoBehaviour
{
    [SerializeField]
    protected Health healthSystem;
    protected void Start()
    {
        healthSystem.OnNoHealth += OnDestruction;
    }

    protected abstract void OnDestruction();
}
