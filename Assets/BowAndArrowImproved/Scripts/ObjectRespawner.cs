using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjectRespawner"))
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;    
        }
    }
}
