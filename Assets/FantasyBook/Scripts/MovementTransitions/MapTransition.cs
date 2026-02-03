using System.Collections;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundry;
    [SerializeField] Direction direction;
    [SerializeField] Transform teleportTargetPosition;
    [SerializeField] float additivePos = 2f;

    private CameraFollow cameraFollow;

    enum Direction { Up, Down, Left, Right, Teleport }

    private void Awake()
    {
        cameraFollow = FindFirstObjectByType<CameraFollow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeTransitionCoroutine(collision.gameObject));

            MoveCharacter move = collision.GetComponent<MoveCharacter>();
            if (move != null)
            {
                move.StopMoving();
            }
        }
    }

    IEnumerator FadeTransitionCoroutine(GameObject player)
    {
        yield return StartCoroutine(ScreenFader.Instance.FadeOutCoroutine());

        UpdatePlayerPosition(player);

        if (cameraFollow != null && mapBoundry != null)
        {
            cameraFollow.UpdateCameraBounds(mapBoundry);
        }

        yield return new WaitForSeconds(0.3f);

        yield return StartCoroutine(ScreenFader.Instance.FadeInCoroutine());

    }

    private void UpdatePlayerPosition(GameObject player)
    {
        if (direction == Direction.Teleport)
        {
            player.transform.position = teleportTargetPosition.position;
            return;
        }

        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += additivePos;
                break;
            case Direction.Down:
                newPos.y -= additivePos;
                break;
            case Direction.Left:
                newPos.x -= additivePos;
                break;
            case Direction.Right:
                newPos.x += additivePos;
                break;
        }

        player.transform.position = newPos;
    }
}