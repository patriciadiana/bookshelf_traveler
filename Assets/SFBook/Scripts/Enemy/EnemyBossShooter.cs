using UnityEngine;

public class EnemyBossShooter : MonoBehaviour
{
    public float fireInterval = 2f;
    public int bulletsPerBurst = 3;
    public float spreadAngle = 30f;
    public string bulletPoolKey = "EnemyBullet";

    private ObjectPool objectPool;
    private Transform playerTransform;

    private void Awake()
    {
        objectPool = FindFirstObjectByType<ObjectPool>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(FireBurst), 1f, fireInterval);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void FireBurst()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        playerTransform = player.transform;

        float startAngle = -spreadAngle / 2f;
        float angleStep = (bulletsPerBurst > 1) ? spreadAngle / (bulletsPerBurst - 1) : 0f;

        for (int i = 0; i < bulletsPerBurst; i++)
        {
            float angleOffset = startAngle + i * angleStep;
            Vector2 direction = GetDirectionTowardsPlayer(angleOffset);
            SpawnBullet(direction);
        }
    }

    private Vector2 GetDirectionTowardsPlayer(float angleOffsetDegrees)
    {
        Vector2 toPlayer = (playerTransform.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;
        float finalAngle = baseAngle + angleOffsetDegrees;
        return new Vector2(Mathf.Cos(finalAngle * Mathf.Deg2Rad), Mathf.Sin(finalAngle * Mathf.Deg2Rad));
    }

    private void SpawnBullet(Vector2 direction)
    {
        GameObject bullet = objectPool.GetObjectFromPool(bulletPoolKey);
        if (bullet == null)
        {
            return;
        }

        bullet.transform.position = transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
            bulletScript.SetDirection(direction);
    }
}
