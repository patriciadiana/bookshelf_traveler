using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;

    private PlayerHealth playerHealth;
    public float attackDamage = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation - 90);

        SoundManager.PlaySound(SoundType.F_FIREBALL);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Destroy(gameObject);
            }
        }
    }
}
