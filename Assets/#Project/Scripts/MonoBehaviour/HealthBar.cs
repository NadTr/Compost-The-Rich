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
    public string gameOverSceneName = "GameOver";
    public string winSceneName = "Win";
    
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
    
    void Update()
    {
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
            Debug.Log("Game Over...");
            
            if (!string.IsNullOrEmpty(gameOverSceneName))
            {
                SceneManager.LoadScene(gameOverSceneName);
            }
            else
            {
                Debug.Log("GameOver scene name is not set");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else if (gameObject.CompareTag("Boss"))
        {
            Debug.Log("You Win!!!!!");
            
            if (!string.IsNullOrEmpty(winSceneName))
            {
                SceneManager.LoadScene(winSceneName);
            }
            else
            {
                Debug.Log("Win scene name is not set");
            }
            
            Destroy(gameObject);
        }
    }
}