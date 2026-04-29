using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBossControl : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float arrivalDistance = 0.1f;

    private Vector2 targetPosition;
    private EnemySpaceshipHealth health;
    private bool isMoving = true;

    public Sprite[] resultCutsceneSlides;
    public float slideDuration = 2f;

    private void Awake()
    {
        health = GetComponent<EnemySpaceshipHealth>();
    }

    private void OnEnable()
    {
        if (health != null) health.ResetHealth();

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        targetPosition = new Vector2(0f, max.y - 3f);

        isMoving = true;
    }

    private void Update()
    {
        if (!isMoving) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPosition) <= arrivalDistance)
        {
            isMoving = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            health.TakeDamage(1u);
            FindFirstObjectByType<ObjectPool>().ReturnObjectToPool("PlayerBullet", collision.gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDeath()
    {
        if (SaveSystem.Instance != null)
        {
            SaveSystem.Instance.allowSaving = false;
            SaveSystem.Instance.DeleteSave();
        }

        CutsceneData.slides = resultCutsceneSlides;
        CutsceneData.slideDuration = slideDuration;
        CutsceneData.nextScene = "MainMenu";

        SceneManager.LoadScene("Cutscene");

        Destroy(gameObject);
    }
}