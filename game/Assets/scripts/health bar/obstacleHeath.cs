using UnityEngine;
using UnityEngine.UI;

public class obstacleHeath : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private Slider slider;

    public void Start()
    {
        slider = GetComponentInChildren<Slider>();
        currentHealth = maxHealth;
        UpdateHealthUI(currentHealth, maxHealth);
    }

    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Boss defeated!");
        }

        Debug.Log($"Current Health after clamping: {currentHealth}");
    }

    private void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        // Debug.Log("Update health called");
        if(slider!=null)
        {
            // Debug.Log("Slider found");
            // Debug.Log("current health: "+ currentHealth);
            // Debug.Log("max health: "+ maxHealth);
            // Debug.Log("Slider value: "+ slider.value);
            slider.value = currentHealth/maxHealth;
            // Debug.Log("Slider value: "+ slider.value);
        }
    }
}
