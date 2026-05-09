using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] private float speed = 40f;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    private Animator animator;
    private Vector2 lastMoveDirection;
    private Vector2 currentMoveDirection;

    public CameraFollow cameraFollow;

    private float footstepTimer;
    private float footstepInterval = 0.4f;

    private bool isAttacking = false;
    private bool canMove = true;

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
        if (canMove && pathVectorList != null && currentPathIndex < pathVectorList.Count)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];

            currentMoveDirection = (targetPosition - transform.position).normalized;

            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", currentMoveDirection.x);
            animator.SetFloat("InputY", currentMoveDirection.y);

            lastMoveDirection = currentMoveDirection;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                SoundManager.PlaySound(SoundType.F_FOOTSTEPS);
                footstepTimer = footstepInterval;
            }

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

    public void StartAttack()
    {
        if (isAttacking) return;

        isAttacking = true;
        StopMoving();

        pathVectorList = null;
        currentPathIndex = 0;

        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    public void SetCanMove(bool value)
    {
        canMove = value;

        if (!canMove)
        {
            StopMoving();

            pathVectorList = null;
            currentPathIndex = 0;
        }
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public bool IsMoving()
    {
        return pathVectorList != null && currentPathIndex < pathVectorList.Count;
    }

    public void PlayFootstep()
    {
        if (IsMoving())
        {
            SoundManager.PlaySound(SoundType.F_FOOTSTEPS);
        }
    }
}