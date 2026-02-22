using UnityEngine;

public class HealthManager : SingletonPersistent<HealthManager>
{
    public float playerHealth;
    public float dragonHealth;

    public float maxHealthPlayer = 5f;
    public float maxHealthDragon = 100f;

    private void Start()
    {
        playerHealth = maxHealthPlayer;
        dragonHealth = maxHealthDragon;
    }

    public void ResetHealth()
    {
        playerHealth = maxHealthPlayer;
        dragonHealth = maxHealthDragon;
    }
}
