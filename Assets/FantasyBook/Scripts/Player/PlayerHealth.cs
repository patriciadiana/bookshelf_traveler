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

        SoundManager.PlaySound(SoundType.F_TAKE_DAMAGE);

        HealthManager.Instance.playerHealth = currentHealth;

        if (currentHealth <= 0)
        {
            GameOverEvent.TriggerGameOver();
        }
    }
}