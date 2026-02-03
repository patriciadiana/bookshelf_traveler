using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Func<Vector3> GetCameraFollowPositionFunc;
    public PolygonCollider2D cameraBoundsCollider;
    public float cameraMoveSpeed = 2f;

    private Camera cam;
    private Bounds bounds;
    private Bounds originalBounds;

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
        originalBounds = cameraBoundsCollider.bounds;

        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        bounds = originalBounds;

        bounds.min += new Vector3(camWidth, camHeight, 0f);
        bounds.max -= new Vector3(camWidth, camHeight, 0f);
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
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        if (originalBounds.size.x <= camWidth * 2f)
        {
            position.x = originalBounds.center.x;
        }
        else
        {
            position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        }

        if (originalBounds.size.y <= camHeight * 2f)
        {
            position.y = originalBounds.center.y;
        }
        else
        {
            position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
        }

        return position;
    }

}