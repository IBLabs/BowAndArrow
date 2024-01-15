using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scenes.Common.Scripts
{
    public class NavMeshEnemyRotator : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 5f;
        
        private NavMeshAgent _agent;
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
        }

        private void Update()
        {
            Vector3 targetDirection = (_agent.steeringTarget - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}