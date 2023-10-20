using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAAIArrowRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private void FixedUpdate()
    {
        transform.forward = Vector3.Slerp(transform.forward, _rigidbody.velocity.normalized, Time.fixedDeltaTime);
    }
}
