using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
        [SerializeField] private Slider bar;

        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
                bar.value = currentHealth / maxHealth;
        }
}