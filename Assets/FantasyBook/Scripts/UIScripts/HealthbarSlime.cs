using UnityEngine;
using UnityEngine.UI;

public class HealthbarSlime : MonoBehaviour
{
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = 3f / 10;
    }

    public void UpdateHealth(float current)
    {
        currentHealthBar.fillAmount = current / 10f;
    }
}