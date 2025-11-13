using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth = 100f;
    public float currentHealth;
    public float damageAmount = 10f;

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

    public void Heal(int healAmount)
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
            Debug.Log("Game Over ...");

            if (!string.IsNullOrEmpty("GameOver"))
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else if (gameObject.CompareTag("Boss"))
        {
            Debug.Log("You Win !!!!!");

            if (!string.IsNullOrEmpty("Win"))
            {
                SceneManager.LoadScene("Win");
            }
            else
            {
                Debug.Log("No WinScene");
            }

            Destroy(gameObject);
        }
    }

}