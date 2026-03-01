using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SidescrollPlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Animator animator;
    private float? targetPositionX = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (targetPositionX == null)
            return;

        float direction = Mathf.Sign(targetPositionX.Value - transform.position.x);

        Vector3 targetPosition = new Vector3(targetPositionX.Value, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        if (Mathf.Abs(transform.position.x - targetPositionX.Value) < 0.05f)
        {
            StopMoving();
        }
    }

    public void SetTargetX(float x)
    {
        targetPositionX = x;
    }

    public void StopMoving()
    {
        /*animator*/
        targetPositionX = null;
    }
}
