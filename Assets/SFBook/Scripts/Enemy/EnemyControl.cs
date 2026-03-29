using UnityEngine;
using UnityEngine.UIElements;

public class EnemyControl : MonoBehaviour
{
    float speed = 2f;
    private int health = 2;

    private void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;

        /* Bottom position */
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if(transform.position.y < min.y)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        FindFirstObjectByType<ObjectPool>().ReturnObjectToPool("EnemySpaceship", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            health--;

            FindFirstObjectByType<ObjectPool>().ReturnObjectToPool("PlayerBullet", collision.gameObject);

            if (health <= 0)
            {
                ReturnToPool();
            }
        }

        if (collision.tag == "Player")
        {
            ReturnToPool();
        }
    }
}
