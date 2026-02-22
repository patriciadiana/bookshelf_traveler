using UnityEngine;
using UnityEngine.UI;

public class HealthbarPlayer : MonoBehaviour
{
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = HealthManager.Instance.playerHealth / 10;
    }

    private void Update()
    {
        currentHealthBar.fillAmount =
            HealthManager.Instance.playerHealth / 10;
    }
}
