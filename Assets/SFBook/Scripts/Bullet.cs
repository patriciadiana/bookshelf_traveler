using UnityEngine;

public enum BulletType
{
    Player,
    Enemy
}

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletType type;
    [SerializeField] private float playerSpeed = 8f;
    [SerializeField] private float enemySpeed = 5f;

    private Vector2 direction;
    private bool isReady;
    private float currentSpeed;

    private void OnEnable()
    {
        if (type == BulletType.Player)
        {
            direction = Vector2.up;
            currentSpeed = playerSpeed;
            isReady = true;
        }
        else
        {
            isReady = false;
            currentSpeed = enemySpeed;
        }
    }

    private void OnDisable()
    {
        isReady = false;
        direction = Vector2.zero;
    }

    public void SetDirection(Vector2 dir)
    {
        if (type == BulletType.Enemy)
        {
            direction = dir.normalized;
            isReady = true;
        }
    }

    private void Update()
    {
        if (!isReady) return;

        transform.position += (Vector3)(direction * currentSpeed * Time.deltaTime);

        Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 max = Camera.main.ViewportToWorldPoint(Vector2.one);

        bool outOfBounds = false;

        if (type == BulletType.Player)
        {
            if (transform.position.y > max.y)
                outOfBounds = true;
        }
        else
        {
            if (transform.position.x < min.x || transform.position.x > max.x ||
                transform.position.y < min.y || transform.position.y > max.y)
            {
                outOfBounds = true;
            }
        }

        if (outOfBounds)
            ReturnToPool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == BulletType.Player && (collision.CompareTag("RedEnemy")
            || collision.CompareTag("GreenEnemy")))
        {
            ReturnToPool();
        }
        else if (type == BulletType.Enemy && collision.CompareTag("Player"))
        {
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        string poolKey = type == BulletType.Player ? "PlayerBullet" : "EnemyBullet";
        FindFirstObjectByType<ObjectPool>().ReturnObjectToPool(poolKey, gameObject);
    }
}
