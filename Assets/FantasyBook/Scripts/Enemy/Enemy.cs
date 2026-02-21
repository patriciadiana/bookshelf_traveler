using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    public GameObject player;
    private MoveCharacter playerMove;
    private Rigidbody2D rb;
    private bool isDead = false;
    private bool isKnockedBack = false;

    private float distance;
    public float speed;
    public float distanceOffset;
    private Vector2 lastPosition;

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockbackForce = 3f;
    private int currentHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = FindFirstObjectByType<MoveCharacter>();
    }

    private void Start()
    {
        lastPosition = transform.position;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isDead || isKnockedBack) return;

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < distanceOffset)
        {
            MoveTowardsPlayer();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    /*Function for when the player leaves the range*/
    private void MoveAwayFromPlayer()
    {
        Vector2 direction = transform.position - player.transform.position;
        direction.Normalize();
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, speed * Time.deltaTime);
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 currentPosition = transform.position;
        float velocity = ((currentPosition - lastPosition) / Time.deltaTime).magnitude;

        lastPosition = currentPosition;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        playerMove.StartAttack();
        playerMove.StopMoving();
        TakeDamage(1);
    }

    public bool CanInteract()
    {
        return !isDead;
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;

        ApplyKnockback();

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy died");
            Destroy(gameObject);
        }
    }

    private void ApplyKnockback()
    {
        StartCoroutine(KnockbackRoutine());
    }

    private IEnumerator KnockbackRoutine()
    {
        isKnockedBack = true;

        Vector2 direction = (transform.position - player.transform.position).normalized;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        isKnockedBack = false;
    }
}
