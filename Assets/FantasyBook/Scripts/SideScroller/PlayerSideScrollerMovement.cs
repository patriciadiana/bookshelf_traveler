using UnityEngine;

public class PlayerSideScrollerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;

    private bool isJumping;

    private Rigidbody2D rb;
    private Animator animator;
    private float moveDirection = 0f;
    private float lastDirection = 1f;

    private bool isAttacking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
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
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
        }
    }

    public void Attack()
    {
        if (isAttacking) return;
        isAttacking = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAttacking) return;

        Debug.Log(isAttacking);

        if (collision.CompareTag("Dragon"))
        {
            DragonHealth dragon = collision.GetComponent<DragonHealth>();
            if (dragon != null)
            {
                dragon.TakeDamage(10f);
                isAttacking = false;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isJumping = false;
        }
    }
}
