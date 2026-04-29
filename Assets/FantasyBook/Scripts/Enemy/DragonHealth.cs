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
        HealthManager.Instance.dragonHealth = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        SoundManager.PlaySound(SoundType.F_DAMAGE);

        HealthManager.Instance.dragonHealth = currentHealth;

        if (currentHealth <= 0)
        {
            /*todo dragon death sound*/
            HandleDragonDeath();
        }
    }

    void HandleDragonDeath()
    {
        CutsceneData.slides = cutsceneSlides;
        CutsceneData.slideDuration = slideDuration;
        CutsceneData.nextScene = nextSceneName;

        SoundManager.PlayMusic(MusicType.DRAGON_VICTORY);
        SceneManager.LoadScene("Cutscene");

        Destroy(gameObject);
    }
}