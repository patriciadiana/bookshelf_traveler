using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = HealthManager.Instance.maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, HealthManager.Instance.maxHealth);

        HealthManager.Instance.playerHealth = currentHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("Player died");
        }
    }
}