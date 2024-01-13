using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WayPointController : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private LayerMask hitMask;

    private int prevSelectedWayPointIndex = -1;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawSphere(transform.position, .3f);

        if (wayPoints.Length == 0) return;

        Gizmos.color = Color.red;
        foreach (var wayPoint in wayPoints)
        {
            Gizmos.DrawSphere(wayPoint.position, 0.3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hitMask.Contains(other.attachedRigidbody.gameObject.layer)) return;

        UpdateNextWayPointIfPossible(other);
    }

    public Transform GetRandomNextWayPoint()
    {
        if (wayPoints.Length == 0)
        {
            throw new Exception($"[ERROR]: wayPoints list is empty in : {gameObject.name}");
        }

        if (wayPoints.Length == 1)
        {
            return wayPoints[0];
        }

        var randomIndex = Random.Range(0, wayPoints.Length);
        while (randomIndex == prevSelectedWayPointIndex) randomIndex = Random.Range(0, wayPoints.Length);
        prevSelectedWayPointIndex = randomIndex;
        return wayPoints[randomIndex];
    }

    private void UpdateNextWayPointIfPossible(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent(out NavMeshAgent agent))
        {
            var newWayPoint = GetRandomNextWayPoint();
            agent.SetDestination(newWayPoint.transform.position);
        }
    }
}