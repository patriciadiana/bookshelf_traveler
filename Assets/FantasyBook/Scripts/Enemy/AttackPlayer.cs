using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public float attackRange = 0.6f;
    public float attackDamage = 0.1f;
    public float attackCooldown = 1.5f;

    private PlayerHealth playerHealth;
    private bool isPlayerInRange = false;
    private float lastAttackTime;

    void Start()
    {
        CircleCollider2D attackCollider = GetComponent<CircleCollider2D>();
        if (attackCollider != null)
        {
            attackCollider.radius = attackRange;
        }
    }

    void Update()
    {
        if (isPlayerInRange && Time.time > lastAttackTime + attackCooldown)
        {
            AttackPlayerFunction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                isPlayerInRange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            playerHealth = null;
        }
    }

    private void AttackPlayerFunction()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            lastAttackTime = Time.time;
        }
    }
}
