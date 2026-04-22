using UnityEngine;
using UnityEngine.SceneManagement;

public class DragonHealth : MonoBehaviour
{
    public float currentHealth { get; private set; }

    public Sprite[] cutsceneSlides;
    public float slideDuration = 2f;
    public string nextSceneName = "_3CrimeBook";

    private void Awake()
    {
        currentHealth = HealthManager.Instance.maxHealthDragon;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        HealthManager.Instance.dragonHealth = currentHealth;

        if (currentHealth <= 0)
        {
            HandleDragonDeath();
        }
    }

    void HandleDragonDeath()
    {
        CutsceneData.slides = cutsceneSlides;
        CutsceneData.slideDuration = slideDuration;
        CutsceneData.nextScene = nextSceneName;

        SceneManager.LoadScene("Cutscene");

        Destroy(gameObject);
    }
}