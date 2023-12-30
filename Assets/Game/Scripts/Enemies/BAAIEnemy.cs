using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BAAIEnemy : MonoBehaviour, BAAIIDeathable
{
    [SerializeField] private UnityEvent<GameObject, int, bool> _onDeath;
    public UnityEvent<GameObject, int, bool> onDeath => _onDeath;
    
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");
    
    [SerializeField] private List<AudioClip> warCryClips;
    [SerializeField] private List<AudioClip> breakClips;

    [Header("Attack Settings")]
    [SerializeField] private float attackRate = 1f;
    [SerializeField] private float attackDamage = 10f;

    [Header("NavMesh Settings")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    
    [SerializeField] private int scoreValue = 1;
    [SerializeField] private string weaponHitTag = "Arrow";

    private IDamageable _target;

    private void Awake()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        agent.isStopped = true;
    }

    private void OnEnable()
    {
        PlayWarCry();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(weaponHitTag)){
            Die(true);
        }
    }

    public void FirstMovementActivation()
    {
        agent.isStopped = false;
    }
    
    public void Die(bool killedByPlayer)
    {
        gameObject.SetActive(false);

        if (killedByPlayer)
        {
            AudioSource.PlayClipAtPoint(breakClips[Random.Range(0, breakClips.Count)], transform.position);
        }
        
        onDeath.Invoke(gameObject, scoreValue, killedByPlayer);
    }

    private void PlayWarCry()
    {
        AudioSource.PlayClipAtPoint(warCryClips[Random.Range(0, warCryClips.Count)], transform.position);
    }

    #region Attack

    public void StartAttacking(IDamageable attackTarget)
    {
        _target = attackTarget;
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
        _target = null;
        agent.isStopped = false;
    }

    private void Attack()
    {
        if (_target != null)
        {
            _target.TakeDamage(attackDamage);
        }
    }

    #endregion
}