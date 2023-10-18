using System;
using UnityEngine;

namespace BowAndArrowImproved.Scripts
{
    public class BAAISimpleMoveTowardsTarget : MonoBehaviour
    {
        public Vector3 targetPosition;

        [SerializeField] private float moveSpeed;

        private void Update()
        {
            MoveTowardsTarget();
        }

        private void MoveTowardsTarget()
        {
            if (targetPosition == null) return;
            transform.forward = (targetPosition - transform.position).normalized;
            transform.position += transform.forward.normalized * (moveSpeed * Time.deltaTime);
        }
    }
}