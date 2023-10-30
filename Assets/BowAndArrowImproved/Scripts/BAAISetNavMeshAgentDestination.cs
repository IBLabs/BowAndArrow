using UnityEngine;
using UnityEngine.AI;

public class BAAISetNavMeshAgentDestination : MonoBehaviour, BAAIINavMeshAgentHolder
{
    [SerializeField] private NavMeshAgent agent;

    [HideInInspector] public Transform target;

    public void SetTargetTransform(Transform target)
    {
        agent.SetDestination(this.target.position);
    }
}
