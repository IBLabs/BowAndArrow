using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WayPointController : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private string enemyTag = "Enemy";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(enemyTag) || wayPoints.Length == 0) return;

        NavMeshAgent agent = other.gameObject.GetComponentInParent<NavMeshAgent>();
        if (agent != null)
        {
            Transform newWayPoint = wayPoints[Random.Range(0, wayPoints.Length)];
            agent.SetDestination(newWayPoint.position);
        }
    }
}