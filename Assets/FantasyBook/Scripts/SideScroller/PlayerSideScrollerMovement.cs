using UnityEngine;

public class PlayerSideScrollerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackDamage = 10f;

    private bool isAttacking;
    private bool isJumping;

    private Rigidbody2D rb;
    private Animator animator;
    private float moveDirection = 0f;
    private float lastDirection = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            moveDirection * moveSpeed,
            rb.linearVelocity.y);
    }

    private void Update()
    {
        if (moveDirection != 0)
            lastDirection = moveDirection;

        animator.SetBool("isWalking", moveDirection != 0);

        float animInputX = moveDirection != 0 ? moveDirection : lastDirection;
        animator.SetFloat("InputX", animInputX);
    }

    public void Jump()
    {
        if (!isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            isJumping = true;
        }
    }

    public void Attack()
    {
        if (isAttacking) return;

        isAttacking = true;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Dragon"))
            {
                DragonHealth dragon = hit.GetComponent<DragonHealth>();
                if (dragon != null)
                {
                    dragon.TakeDamage(attackDamage);
                }
            }
        }

        isAttacking = false;
    }

    public void MoveLeft()
    {
        moveDirection = -1f;
        lastDirection = -1f;
    }

    public void MoveRight()
    {
        moveDirection = 1f;
        lastDirection = 1f;
    }

    public void StopMoving()
    {
        moveDirection = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isJumping = false;
        }
    }
}