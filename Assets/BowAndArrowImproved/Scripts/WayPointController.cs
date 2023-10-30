using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WayPointController : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private LayerMask hitMask;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!hitMask.Contains(other.attachedRigidbody.gameObject.layer)) return;

        if (wayPoints.Length == 0)
        {
            throw new Exception($"[ERROR]: wayPoints list is empty in : {gameObject.name}");
        }
        
        if (other.attachedRigidbody.TryGetComponent(out NavMeshAgent agent))
        {
            Transform newWayPoint = wayPoints[Random.Range(0, wayPoints.Length)];
            agent.SetDestination(newWayPoint.position);
        }
    }
}