using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BAAIDamageable : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private TMP_Text healthBarText;
    private float currentHealth;

    [Header("Events")]
    public UnityEvent OnTakeDamage;
    public UnityEvent OnHeal;
    public UnityEvent OnDeath;
    
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBarText.text = "Health\n" + currentHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBarText.text = "Health\n" + currentHealth;
        OnTakeDamage?.Invoke();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnHeal?.Invoke();
    }

    private void Die()
    {
        OnDeath?.Invoke();
        
        // Additional logic for dying can be added here. For example, destroying the game object.
        // Destroy(gameObject);
    }
}