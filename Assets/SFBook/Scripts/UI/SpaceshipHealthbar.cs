using UnityEngine;
using UnityEngine.UI;

public class SpaceshipHealthbar : MonoBehaviour
{
    [SerializeField] private Image totalHealthBar;   
    [SerializeField] private Image currentHealthBar; 

    private void OnEnable()
    {
        Spaceship.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        Spaceship.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float currentHealth)
    { 
        currentHealthBar.fillAmount = currentHealth / 10;
    }
}