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
        Debug.Log("after compare  " + other.gameObject.tag);
        NavMeshAgent agent = other.gameObject.GetComponentInParent<NavMeshAgent>();

        if (agent != null)
        {
            Debug.Log("in get component  " + other.gameObject.tag);
            Transform newWayPoint = wayPoints[Random.Range(0, wayPoints.Length)];
            Debug.Log("beforeSetDstination  " + newWayPoint.position);
            agent.SetDestination(newWayPoint.position);
            Debug.Log("after setDestination  " + agent.destination);
        }
    }
}