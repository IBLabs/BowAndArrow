using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation.Samples;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(AgentLinkMover))]
public class EnemyAnimationsHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AgentLinkMover linkMover;
    
    private static readonly int HeyJump = Animator.StringToHash("HeyJump");
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int FrontFlip = Animator.StringToHash("FrontFlip");
    private static readonly int LookAround = Animator.StringToHash("LookAround");
    
    private void Awake()
    {
        linkMover = GetComponent<AgentLinkMover>();

        linkMover.OnLinkStart += HandleLinkStart;
        linkMover.OnLinkEnd += HandleLinkEnd;
    }

    private void HandleLinkStart()
    {
        animator.SetTrigger(FrontFlip);
    }

    private void HandleLinkEnd()
    {
        animator.SetTrigger(Run);
    }
}
