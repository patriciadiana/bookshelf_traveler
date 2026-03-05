using UnityEngine;
using UnityEngine.SceneManagement;

public class DragonHealth : MonoBehaviour
{
   public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = HealthManager.Instance.maxHealthDragon;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        HealthManager.Instance.dragonHealth = currentHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("_3CrimeBook");
        }
    }
}
