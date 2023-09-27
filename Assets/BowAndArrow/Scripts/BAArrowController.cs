using System;
using System.Collections;
using UnityEngine;

namespace BowAndArrow.Scripts
{
    public class BAArrowController : MonoBehaviour
    {
        [SerializeField] private Transform tipTransform;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Collider arrowCollider;
        
        [SerializeField] private float speed;

        private bool _isInAir;

        private void OnEnable()
        {
            BAPullInteractable.PullDidRelease += OnPullDidRelease;
        }

        private void OnDisable()
        {
            BAPullInteractable.PullDidRelease -= OnPullDidRelease;
        }

        private void OnPullDidRelease(float amount)
        {
            BAPullInteractable.PullDidRelease -= OnPullDidRelease;
            
            transform.parent = null;
            rb.isKinematic = false;

            Vector3 force = transform.forward * speed * amount;
            rb.AddForce(force, ForceMode.Impulse);
            
            _isInAir = true;
            StartCoroutine(RotateWithVelocityCoroutine());
        }

        private IEnumerator RotateWithVelocityCoroutine()
        {
            while (_isInAir)
            {
                Quaternion newRotation = Quaternion.LookRotation(rb.velocity, transform.up);
                transform.rotation = newRotation;
                yield return null;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            _isInAir = false;
            transform.parent = other.transform;
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;
        }
    }
}