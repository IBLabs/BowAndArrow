using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WayPointController : MonoBehaviour
{
    [SerializeField] private WayPointController[] wayPoints;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private bool isEndPoint;
    
    private void OnTriggerEnter(Collider other)
    {
        if (isEndPoint || !hitMask.Contains(other.attachedRigidbody.gameObject.layer)) return;
        
        UpdateNextWayPointIfPossible(other);
    }


    public WayPointController GetRandomNextWayPoint()
    {
        if (wayPoints.Length == 0) throw new Exception($"[ERROR]: wayPoints list is empty in : {gameObject.name}");
        
        return wayPoints[Random.Range(0, wayPoints.Length)];
    }

    private void UpdateNextWayPointIfPossible(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent(out NavMeshAgent agent))
        {
            WayPointController newWayPoint = GetRandomNextWayPoint();
            agent.SetDestination(newWayPoint.transform.position);
        }
    }


}