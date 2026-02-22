using UnityEngine;
using UnityEngine.UI;

public class HealthbarDragon : MonoBehaviour
{
    [SerializeField] private Image totalHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = HealthManager.Instance.dragonHealth;
    }

    private void Update()
    {
        totalHealthBar.fillAmount =
            HealthManager.Instance.dragonHealth / 100f;
    }
}
