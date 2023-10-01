using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BAAISetNavMeshAgentDestination : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [HideInInspector] public Transform target;

    void Update()
    {
        agent.destination = target.position;
    }
}
