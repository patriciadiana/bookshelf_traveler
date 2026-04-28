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
    private float damageTaken = 1f;

    [SerializeField] private float knockbackForce = 3f;
    [SerializeField] private float maxHealthSlime = 3f;
    private HealthbarSlime healthbar;
    private float currentHealth;

    [SerializeField] private float minMoveSpeedForSound = 0.1f;
    [SerializeField] private Vector2 moveSoundCooldownRange = new Vector2(0.3f, 0.7f);

    private float lastMoveSoundTime;
    private float currentMoveCooldown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = FindFirstObjectByType<MoveCharacter>();

        healthbar = GetComponentInChildren<HealthbarSlime>();
    }

    private void Start()
    {
        lastPosition = transform.position;
        currentHealth = maxHealthSlime;

        healthbar.UpdateHealth(currentHealth);
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

        float currentSpeed = rb.linearVelocity.magnitude;

        if (currentSpeed > minMoveSpeedForSound &&
            Time.time > lastMoveSoundTime + currentMoveCooldown)
        {
            SoundManager.PlaySound(SoundType.F_SLIME_MOVE);

            lastMoveSoundTime = Time.time;

            currentMoveCooldown = Random.Range(
                moveSoundCooldownRange.x,
                moveSoundCooldownRange.y
            );
        }
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
        TakeDamage(damageTaken);
    }

    public bool CanInteract()
    {
        return !isDead;
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;

        SoundManager.PlaySound(SoundType.F_DAMAGE);

        healthbar.UpdateHealth(currentHealth);

        ApplyKnockback();

        if (currentHealth <= 0)
        {
            SoundManager.PlaySound(SoundType.F_SLIME_DEATH);
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
