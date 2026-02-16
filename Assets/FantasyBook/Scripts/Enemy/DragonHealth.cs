using UnityEngine;

public class DragonHealth : MonoBehaviour
{
   public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = HealthManager.Instance.maxHealthEnemy;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        HealthManager.Instance.enemyHealth = currentHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("Dragon died");
        }
    }
}
