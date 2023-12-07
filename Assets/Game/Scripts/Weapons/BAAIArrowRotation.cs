using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAAIArrowRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private void FixedUpdate()
    {
        Vector3 forward = _rigidbody.velocity.normalized;
        
        if (forward == Vector3.zero) return;
        
        transform.forward = forward;
    }
}
