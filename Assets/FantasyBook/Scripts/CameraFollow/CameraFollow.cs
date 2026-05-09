using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour, ISaveable
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

    public void SaveData(GameSaveData saveData)
    {
        if (saveData.fantasyData == null)
            saveData.fantasyData = new FantasySaveData();

        if (cameraBoundsCollider != null)
        {
            saveData.fantasyData.cameraBoundryName =
                cameraBoundsCollider.gameObject.name;
        }
    }

    public void LoadData(GameSaveData saveData)
    {
        if (saveData.fantasyData == null)
            return;

        if (string.IsNullOrEmpty(saveData.fantasyData.cameraBoundryName))
            return;

        GameObject boundaryObj =
            GameObject.Find(saveData.fantasyData.cameraBoundryName);

        if (boundaryObj == null)
        {
            Debug.LogWarning("Saved camera boundary not found in scene.");
            return;
        }

        PolygonCollider2D collider =
            boundaryObj.GetComponent<PolygonCollider2D>();

        if (collider != null)
        {
            if (saveData.fantasyData.cameraBoundryName == "BoundryCaveInterior")
            {
                SoundManager.PlayMusic(MusicType.CAVE_AMBIENT);
            }
            else
            {
                SoundManager.PlayMusic(MusicType.FANTASY_AMBIENT);
            }

            UpdateCameraBounds(collider);
        }
    }
}