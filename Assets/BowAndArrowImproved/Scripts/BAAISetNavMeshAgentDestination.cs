using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = System.Numerics.Vector3;

public class BAAISetNavMeshAgentDestination : MonoBehaviour, BAAIINavMeshAgentHolder
{
    [SerializeField] private NavMeshAgent agent;

    [HideInInspector] public Transform target;

    void Update()
    {
        agent.destination = target.position;
    }

    public void SetTargetTransform(Transform target)
    {
        this.target = target;
    }
}
