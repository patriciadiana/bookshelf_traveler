using TMPro;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int enemiesToSpawn;

    public GameObject[] enemyPrefabs;
    public string[] enemyTags;

    public float spawnInterval;
    public bool isBossWave = false;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public GameObject bossPrefab;

    private ObjectPool objectPool;

    private int currentWaveNumber = 0;
    private float nextSpawnTime;

    private bool canSpawn = true;
    private int enemiesRemainingToSpawn;

    public TextMeshProUGUI waveText;

    private void Start()
    {
        objectPool = ObjectPool.Instance;
        StartWave();
    }

    private void Update()
    {
        SpawnWave();

        int totalEnemies = 0;

        foreach (string tag in waves[currentWaveNumber].enemyTags)
        {
            totalEnemies += GameObject.FindGameObjectsWithTag(tag).Length;
        }

        if (totalEnemies == 0 && !canSpawn && currentWaveNumber + 1 < waves.Length)
        {
            SpawnNextWave();
        }
    }

    private void StartWave()
    {
        enemiesRemainingToSpawn = waves[currentWaveNumber].enemiesToSpawn;
        canSpawn = true;

        UpdateWaveText();
    }

    private void SpawnNextWave()
    {
        currentWaveNumber++;
        StartWave();
    }

    private void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = $"{waves[currentWaveNumber].waveName}";
        }
    }

    private void SpawnWave()
    {
        Wave currentWave = waves[currentWaveNumber];

        if (currentWave.isBossWave)
        {
            if (canSpawn && enemiesRemainingToSpawn > 0)
            {
                SpawnBoss();
                enemiesRemainingToSpawn = 0;
                canSpawn = false;
            }
            return;
        }

        if (!canSpawn || Time.time < nextSpawnTime)
            return;

        int index = Random.Range(0, currentWave.enemyPrefabs.Length);

        string tag = currentWave.enemyTags[index];

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 spawnPos = new Vector2(Random.Range(min.x, max.x), max.y);

        GameObject enemy = objectPool.GetObjectFromPool(tag);
        enemy.transform.position = spawnPos;

        if (enemy != null)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;

            if (enemiesRemainingToSpawn <= 0)
            {
                canSpawn = false;
            }
        }
    }

    private void SpawnBoss()
    {
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 spawnPosition = new Vector2(0f, max.y + 3f);

        Instantiate(bossPrefab, spawnPosition, Quaternion.Euler(0, 0, 180));

        SoundManager.PlayMusic(MusicType.SF_BOSS_BATTLE);
    }
}