using UnityEngine;
using UnityEngine.AI;

public class BAAISetNavMeshAgentDestination : MonoBehaviour, BAAIINavMeshAgentHolder
{
    [SerializeField] private NavMeshAgent agent;

    [HideInInspector] public Transform target;

    void Start()
    {
        agent.destination = target.position;
    }

    public void SetTargetTransform(Transform target)
    {
        this.target = target;
    }
}
