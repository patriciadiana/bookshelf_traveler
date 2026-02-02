using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float startingHealth;
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = startingHealth;
    }
    private void Start()
    {
        ResetHealth();
    }

    public void TakeDamange(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {

        }
        else
        {
            Debug.Log("Player died");
        }
    }

    public void ResetHealth()
    {
        currentHealth = startingHealth;
    }
}