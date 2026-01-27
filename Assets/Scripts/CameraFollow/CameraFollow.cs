using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Func<Vector3> GetCameraFollowPositionFunc;
    public PolygonCollider2D cameraBoundsCollider;
    public float cameraMoveSpeed = 2f;

    private Camera cam;
    private Bounds bounds;

    private void Start()
    {
        cam = GetComponent<Camera>();
        if (cameraBoundsCollider != null)
        {
            CalculateCameraBounds();
        }
    }

    private void CalculateCameraBounds()
    {
        bounds = cameraBoundsCollider.bounds;

        float cameraHeight = cam.orthographicSize * 2f;
        float cameraWidth = cameraHeight * cam.aspect;

        bounds.min = new Vector3(
            bounds.min.x + cameraWidth / 2f,
            bounds.min.y + cameraHeight / 2f,
            bounds.min.z
        );

        bounds.max = new Vector3(
            bounds.max.x - cameraWidth / 2f,
            bounds.max.y - cameraHeight / 2f,
            bounds.max.z
        );
    }

    public void UpdateCameraBounds(PolygonCollider2D newBounds)
    {
        cameraBoundsCollider = newBounds;
        CalculateCameraBounds();
    }

    public void Setup(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }

    public void SetGetCameraPositionFunc(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }

    private void Update()
    {
        if (GetCameraFollowPositionFunc == null) return;

        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 targetPosition = Vector3.Lerp(transform.position, cameraFollowPosition, cameraMoveSpeed * Time.deltaTime);

        if (cameraBoundsCollider != null)
        {
            targetPosition = ClampToBounds(targetPosition);
        }

        transform.position = targetPosition;
    }

    private Vector3 ClampToBounds(Vector3 position)
    {
        if (bounds.size == Vector3.zero && cameraBoundsCollider != null)
        {
            CalculateCameraBounds();
        }

        position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);

        return position;
    }
}