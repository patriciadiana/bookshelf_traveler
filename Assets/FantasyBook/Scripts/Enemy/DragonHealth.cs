using UnityEngine;

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
            Debug.Log("Dragon died");
            Destroy(gameObject);
        }
    }
}
