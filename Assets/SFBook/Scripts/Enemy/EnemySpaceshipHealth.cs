using UnityEngine;

public class EnemySpaceshipHealth : MonoBehaviour
{
    public float maxHealth = 5f;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
