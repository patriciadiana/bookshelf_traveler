using UnityEngine;

public class CameraFollowSideScroller : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset;

    [Header("Boundary Settings")]
    public bool useBoundary = false;
    public PolygonCollider2D boundaryCollider;

    private Bounds boundaryBounds;

    void Start()
    {
        if (boundaryCollider != null)
        {
            CalculateBoundaryBounds();
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            transform.position.y,
            transform.position.z
        );

        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        if (useBoundary && boundaryCollider != null)
        {
            smoothedPosition = ConstrainToBoundary(smoothedPosition);
        }

        transform.position = smoothedPosition;
    }

    Vector3 ConstrainToBoundary(Vector3 position)
    {

        float cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        float minBoundaryX = boundaryBounds.min.x + cameraHalfWidth;
        float maxBoundaryX = boundaryBounds.max.x - cameraHalfWidth;

        position.x = Mathf.Clamp(position.x, minBoundaryX, maxBoundaryX);

        return position;
    }

    void CalculateBoundaryBounds()
    {
        if (boundaryCollider != null)
        {
            boundaryBounds = boundaryCollider.bounds;
        }
    }

    public void UpdateBoundaryBounds()
    {
        CalculateBoundaryBounds();
    }
}