using TMPro;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int enemiesToSpawn;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    private ObjectPool objectPool;

    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;

    private bool canSpawn = true;

    public TextMeshProUGUI waveText;

    private void Start()
    {
        UpdateWaveText();
        objectPool = FindFirstObjectByType<ObjectPool>();
    }

    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("EnemySpaceship");

        if(totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 != waves.Length)
        {
            SpawnNextWave();
        }
    }

    private void SpawnNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;

        UpdateWaveText();
    }

    private void UpdateWaveText()
    {
        waveText.text = waves[currentWaveNumber].waveName;
    }

    private void SpawnWave()
    {
        GameObject enemyPrefab = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
        if(canSpawn && nextSpawnTime < Time.time)
        {
            if (objectPool != null)
            {
                Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
                Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

                GameObject enemy = objectPool.GetObjectFromPool(enemyPrefab.name,
                    new Vector2(Random.Range(min.x, max.x), max.y));

                currentWave.enemiesToSpawn--;

                nextSpawnTime = Time.time + currentWave.spawnInterval;

                if(currentWave.enemiesToSpawn == 0)
                {
                    canSpawn = false;
                }
            }
        }
    }

}
