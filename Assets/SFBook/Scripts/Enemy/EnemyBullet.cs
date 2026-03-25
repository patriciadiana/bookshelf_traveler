using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed = 5f;
    Vector2 direction;
    bool isReady = false;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        isReady = true;
    }

    private void Update()
    {
        if (isReady)
        {
            Vector2 position = transform.position;

            position += direction * speed * Time.deltaTime;

            transform.position = position;

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            if ((transform.position.x < min.x) || (transform.position.x > max.x) ||
            (transform.position.y < min.y) || (transform.position.y > max.y))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player"))
        {
            Destroy(gameObject);
        }
    }
}
