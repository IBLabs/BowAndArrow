using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTargetGizmo : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        if (TryGetComponent<NavMeshAgent>(out var agent))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(agent.destination, 0.5f);    
        }
    }
}
