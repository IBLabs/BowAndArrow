using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BAAIEnemy : MonoBehaviour,BAAIIDeathable
{
    [SerializeField] private UnityEvent<GameObject> _onDeath;
    [SerializeField] private List<AudioClip> breakClips;

    [Header("Attack Settings")]
    public float attackRate = 1f;
    public float attackDamage = 10f;

    [Header("NavMesh Settings")]
    public NavMeshAgent agent;
    public Animator animator;

    public UnityEvent<GameObject> onDeath => _onDeath;

    private IDamageable target;
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    [SerializeField] private List<AudioClip> warCryClips;

    private void OnEnable()
    {
        PlayWarCry();
    }

    private void PlayWarCry()
    {
        AudioSource.PlayClipAtPoint(warCryClips[Random.Range(0, warCryClips.Count)], transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        onDeath.Invoke(gameObject);
        gameObject.SetActive(false);
        
        AudioSource.PlayClipAtPoint(breakClips[Random.Range(0, breakClips.Count)], transform.position);
    }

    private void Awake()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    public void StartAttacking(IDamageable attackTarget)
    {
        target = attackTarget;
        agent.isStopped = true;

        if (animator != null)
        {
            animator.SetTrigger(AttackTrigger);    
        }

        InvokeRepeating(nameof(Attack), 0f, attackRate);
    }

    public void StopAttacking()
    {
        CancelInvoke(nameof(Attack));
        target = null;
        agent.isStopped = false;
    }

    private void Attack()
    {
        if (target != null)
        {
            target.TakeDamage(attackDamage);
        }
    }
}