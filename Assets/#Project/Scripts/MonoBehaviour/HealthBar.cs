using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;

    void Start()
    {

    }

    
    void Update()
    {
        if (healthAmount <= 0) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Keyboard.current.enterKey.wasPressedThisFrame) 
        {
            TakeDamage(10);
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame) 
        {
            Heal(5);
        }
    }
    public void TakeDamage(float damage) 
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;

    }

    public void Heal(float healingAmount) 
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); 
        healthBar.fillAmount = healthAmount/100f; 
    }
}
