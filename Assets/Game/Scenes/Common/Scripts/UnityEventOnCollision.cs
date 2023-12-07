using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventOnCollision : MonoBehaviour
{
    public UnityEvent<Collision> onCollisionEnter;

    private void OnCollisionEnter(Collision other)
    {
        onCollisionEnter?.Invoke(other);
    }
}
