using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DetectionArea : MonoBehaviour
{
    public event Action<Collider2D> OnAreaEnter, OnAreaStay, OnAreaExit;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnAreaEnter?.Invoke(collision);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        OnAreaStay?.Invoke(collision);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        OnAreaExit?.Invoke(collision);
    }
}
