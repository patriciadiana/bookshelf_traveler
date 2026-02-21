using UnityEngine;

public class SlimeSpawn : MonoBehaviour
{
    public GameObject slimePrefab;
    public Transform[] spawnPoints;
    public int enemyCount = 3;

    private void Start()
    {
        SpawnSlimes();
    }

    void SpawnSlimes()
    {
        float radius = 0.6f;

        foreach (Transform spawnPoint in spawnPoints)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                float angle = i * Mathf.PI * 2 / enemyCount;
                Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

                Instantiate(slimePrefab, (Vector2)spawnPoint.position + offset, Quaternion.identity);
            }
        }
    }
}
