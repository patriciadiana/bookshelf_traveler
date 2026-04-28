using System.Collections;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundry;
    [SerializeField] Direction direction;
    [SerializeField] AreaType areaType;
    [SerializeField] Transform teleportTargetPosition;
    [SerializeField] float additivePos = 2f;

    private CameraFollow cameraFollow;

    enum Direction { Up, Down, Left, Right, Teleport }

    enum AreaType { None, Cave, Town }

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
        }
    }

    IEnumerator FadeTransitionCoroutine(GameObject player)
    {
        MoveCharacter move = player.GetComponent<MoveCharacter>();

        if (move != null)
        {
            move.SetCanMove(false);   
            move.StopMoving();       
        }

        yield return StartCoroutine(ScreenFader.Instance.FadeOutCoroutine());

        UpdatePlayerPosition(player);

        switch (areaType)
        {
            case AreaType.Cave:
                SoundManager.PlayMusic(MusicType.CAVE_AMBIENT);
                break;

            case AreaType.Town:
                SoundManager.PlayMusic(MusicType.FANTASY_AMBIENT);
                break;
        }

        if (cameraFollow != null && mapBoundry != null)
        {
            cameraFollow.UpdateCameraBounds(mapBoundry);
        }

        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(ScreenFader.Instance.FadeInCoroutine());

        if (move != null)
        {
            move.SetCanMove(true);
        }
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