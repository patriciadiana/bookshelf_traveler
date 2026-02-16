using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = HealthManager.Instance.maxHealthPlayer;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        HealthManager.Instance.playerHealth = currentHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("Player died");
        }
    }
}