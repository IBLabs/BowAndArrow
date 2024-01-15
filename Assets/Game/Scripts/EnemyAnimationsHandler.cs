using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scenes.Common.Scripts;
using Samples.AI_Navigation._1._1._4.Build_And_Connect_NavMesh_Surfaces.Scripts;
using Unity.AI.Navigation.Samples;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(CustomAgentLinkMover))]
public class EnemyAnimationsHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CustomAgentLinkMover linkMover;
    
    private static readonly int HeyJump = Animator.StringToHash("HeyJump");
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int FrontFlip = Animator.StringToHash("FrontFlip");
    private static readonly int LookAround = Animator.StringToHash("LookAround");
    
    private void Awake()
    {
        linkMover = GetComponent<CustomAgentLinkMover>();

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
