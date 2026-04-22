using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public Image slideImage;

    private Sprite[] slides;
    private float duration;
    private string nextScene;

    private int index = 0;

    void Start()
    {
        SaveSystem.Instance.allowSaving = false;

        slides = CutsceneData.slides;
        duration = CutsceneData.slideDuration;
        nextScene = CutsceneData.nextScene;

        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        while (index < slides.Length)
        {
            slideImage.sprite = slides[index];
            yield return new WaitForSeconds(duration);
            index++;
        }

        LoadNextScene();
    }

    public void SkipCutscene()
    {
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
        SaveSystem.Instance.allowSaving = true;
    }
}