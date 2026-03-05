using UnityEngine;

public class SidescrollPlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;

    private Animator animator;
    private float? targetPositionX = null;
    private float lastDirection = 1f;
    private bool canMove = true;

    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool isWalking = targetPositionX != null;
        animator.SetBool("isWalking", isWalking);

        float animInputX = isWalking ? Mathf.Sign(targetPositionX.Value - transform.position.x) : lastDirection;
        animator.SetFloat("InputX", animInputX);

        if (isWalking)
            lastDirection = Mathf.Sign(targetPositionX.Value - transform.position.x);
    }

    private void FixedUpdate()
    {
        if (!canMove || EvidenceBoardPopup.Instance.IsOpen())
        {
            StopMoving();
            animator.SetBool("isWalking", false);
            return;
        }

        HandleMovement();
    }

    private void HandleMovement()
    {
        if (targetPositionX == null)
            return;

        float direction = Mathf.Sign(targetPositionX.Value - rb.position.x);

        Vector2 newPosition = Vector2.MoveTowards(
            rb.position,
            new Vector2(targetPositionX.Value, rb.position.y),
            speed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPosition);

        if (Mathf.Abs(rb.position.x - targetPositionX.Value) < 0.05f)
        {
            StopMoving();
        }
    }

    public void SetTargetX(float x)
    {
        if (!canMove || EvidenceBoardPopup.Instance.IsOpen())
        {
            targetPositionX = null;
            return;
        }

        targetPositionX = x;
    }

    public void StopMoving()
    {
        targetPositionX = null;
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
        if (!canMove) StopMoving();
    }
}