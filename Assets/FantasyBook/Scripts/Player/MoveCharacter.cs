using UnityEngine;
using System.Collections.Generic;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] private float speed = 40f;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    private Animator animator;
    private Vector2 lastMoveDirection;
    private Vector2 currentMoveDirection;

    public CameraFollow cameraFollow;
    private void Start()
    {
        animator = GetComponent<Animator>();
        cameraFollow.Setup(() => transform.position);
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (pathVectorList != null && currentPathIndex < pathVectorList.Count)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];

            currentMoveDirection = (targetPosition - transform.position).normalized;

            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", currentMoveDirection.x);
            animator.SetFloat("InputY", currentMoveDirection.y);

            lastMoveDirection = currentMoveDirection;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPathIndex++;

                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }

    public void StopMoving()
    {
        animator.SetBool("isWalking", false);

        animator.SetFloat("LastInputX", lastMoveDirection.x);
        animator.SetFloat("LastInputY", lastMoveDirection.y);

        pathVectorList = null;
        currentPathIndex = 0;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(List<Vector3> path)
    {
        if (path == null || path.Count == 0)
        {
            return;
        }

        currentPathIndex = 0;
        pathVectorList = new List<Vector3>(path);
    }

    public bool IsMoving()
    {
        return pathVectorList != null && currentPathIndex < pathVectorList.Count;
    }
}