using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class ScreenFader : Singleton<ScreenFader>
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration = 0.5f;

    IEnumerator Fade(float targetTransparency)
    {
        float start = canvasGroup.alpha;
        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, targetTransparency, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = targetTransparency;
    }

    public IEnumerator FadeOutCoroutine()
    {
        yield return StartCoroutine(Fade(1));
    }

    public IEnumerator FadeInCoroutine()
    {
        yield return StartCoroutine(Fade(0));
    }
}
