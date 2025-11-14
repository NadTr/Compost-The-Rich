using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    [Header("Health Settings")]
    public Image healthBar;
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Scenes")]
    public string gameOverSceneName = "Menu_Game_Over";
    public string winSceneName = "Menu_Victory";

    void Start()
    {
        currentHealth = maxHealth;
        // UpdateHealthBar();
    }

    void Update()
    {
        // Debug.Log("currentHealth = " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Menu_Game_Over");
        }
        else if (gameObject.CompareTag("Boss"))
        {
            SceneManager.LoadScene("Menu_Victory");
            Destroy(gameObject);
        }
    }
}