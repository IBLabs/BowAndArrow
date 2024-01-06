using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider bar;
    [SerializeField] private Image healthLine;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float healthRatio = currentHealth / maxHealth;

        bar.value = healthRatio;
        if (healthRatio > 0.5f)
        {
            healthLine.color = Color.Lerp(Color.yellow, Color.green, 2 * (healthRatio - 0.5f));
        }
        else
        {
            healthLine.color = Color.Lerp(Color.red, Color.yellow, 2 * healthRatio);
        }
    }
}