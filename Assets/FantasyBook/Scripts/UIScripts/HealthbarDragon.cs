using UnityEngine;
using UnityEngine.UI;

public class HealthbarDragon : MonoBehaviour
{
    [SerializeField] private Image totalHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = HealthManager.Instance.enemyHealth;
    }

    private void Update()
    {
        totalHealthBar.fillAmount =
            HealthManager.Instance.enemyHealth / 100f;
    }
}
