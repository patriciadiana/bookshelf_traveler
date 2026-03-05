using System.Collections;
using UnityEngine;

public class SceneTransitions : MonoBehaviour
{
    IEnumerator FadeTransitionCoroutine(GameObject player)
    {
        yield return StartCoroutine(ScreenFader.Instance.FadeOutCoroutine());

        

        yield return new WaitForSeconds(0.3f);

        yield return StartCoroutine(ScreenFader.Instance.FadeInCoroutine());

    }
}
