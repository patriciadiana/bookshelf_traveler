using UnityEngine;

public class HealthManager : SingletonPersistent<HealthManager>
{
    public float playerHealth;
    public float enemyHealth;
    public float maxHealthPlayer = 5f;
    public float maxHealthEnemy = 100f;

    private void Start()
    {
        playerHealth = maxHealthPlayer;
        enemyHealth = maxHealthEnemy;
    }

    public void ResetHealth()
    {
        playerHealth = maxHealthPlayer;
        enemyHealth = maxHealthEnemy;
    }
}
