using System.Collections;
using UnityEngine;

public class SceneTransitions : Singleton<SceneTransitions>
{
    public void StartTransition(GameObject player, Transform targetPosition, PolygonCollider2D newCollider)
    {
        StartCoroutine(FadeTransitionCoroutine(player, targetPosition, newCollider));
    }

    public void StartTransitionWithoutPlayer(GameObject player, PolygonCollider2D newCollider)
    {
        StartCoroutine(FadeTransitionWithoutPlayerCoroutine(player, newCollider));
    }

    IEnumerator FadeTransitionWithoutPlayerCoroutine(GameObject player, PolygonCollider2D newCollider)
    {
        yield return StartCoroutine(ScreenFader.Instance.FadeOutCoroutine());

        player.GetComponent<SidescrollPlayerMovement>().enabled = false;

        if (newCollider != null)
        {
            CameraFollowSideScroller cam = Camera.main.GetComponent<CameraFollowSideScroller>();
            cam.boundaryCollider = newCollider;
            cam.UpdateBoundaryBounds();
        }

        yield return new WaitForSeconds(0.3f);

        yield return StartCoroutine(ScreenFader.Instance.FadeInCoroutine());
    }

    IEnumerator FadeTransitionCoroutine(GameObject player, Transform targetPosition, PolygonCollider2D newCollider)
    {
        yield return StartCoroutine(ScreenFader.Instance.FadeOutCoroutine());

        player.transform.position = targetPosition.position;

        if (newCollider != null)
        {
            CameraFollowSideScroller cam = Camera.main.GetComponent<CameraFollowSideScroller>();
            cam.boundaryCollider = newCollider;
            cam.UpdateBoundaryBounds();
        }

        yield return new WaitForSeconds(0.3f);

        yield return StartCoroutine(ScreenFader.Instance.FadeInCoroutine());
    }
}
