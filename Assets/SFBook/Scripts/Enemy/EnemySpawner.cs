using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    float maxSpawnRateInSeconds = 5f;

    private void Start()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        /*Every 15 seconds difficulty increases*/
        InvokeRepeating("IncreaseSpawnRate", 0f, 15f);
    }

    private void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject enemyGameObject = Instantiate(enemy);
        enemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextEnemySpawn();
    }

    private void ScheduleNextEnemySpawn()
    {
        float secondsWhenToSpawn;

        if(maxSpawnRateInSeconds > 1f)
        {
            secondsWhenToSpawn = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
        {
            secondsWhenToSpawn = 1f;
        }

        Invoke("SpawnEnemy", secondsWhenToSpawn);
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;

        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }
}
