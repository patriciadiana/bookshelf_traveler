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
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;

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
        waveText.text = waves[currentWaveNumber].waveName;
    }

    private void SpawnWave()
    {
        Wave currentWave = waves[currentWaveNumber];

        if (!canSpawn || Time.time < nextSpawnTime)
            return;

        int index = Random.Range(0, currentWave.enemyPrefabs.Length);

        string tag = currentWave.enemyTags[index];

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 spawnPos = new Vector2(Random.Range(min.x, max.x), max.y);

        GameObject enemy = objectPool.GetObjectFromPool(tag, spawnPos);

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
}