using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemySpaceship : MonoBehaviour
{
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private EnemySpaceshipHealth health;

    private void Start()
    {
        totalHealthBar.fillAmount = 1f;
    }

    private void Update()
    {
        totalHealthBar.fillAmount =
            health.GetCurrentHealth() / health.maxHealth;
    }
}
