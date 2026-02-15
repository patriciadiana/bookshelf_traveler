using UnityEngine;

public class HealthManager : SingletonPersistent<HealthManager>
{
    public float playerHealth;
    public float maxHealth = 5f;

    private void Start()
    {
        playerHealth = maxHealth;
    }

    public void ResetHealth()
    {
        playerHealth = maxHealth;
    }
}
