using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Sprite[] introSlides;
    public float slideDuration = 2f;
    public string nextSceneName = "_1FantasyBook";

    public GameObject optionsMenu;

    public void OnPlayButton()
    {
        GameSaveData save = SaveSystem.Instance.GetSaveData();

        if (save == null || save.currentScene == "_1FantasyBook")
        {
            CutsceneData.slides = introSlides;
            CutsceneData.slideDuration = slideDuration;
            CutsceneData.nextScene = nextSceneName;

            SceneManager.LoadScene("Cutscene");
        }
        else
        {
            SceneManager.LoadScene(save.currentScene);
        }
    }

    public void OnOptionsButton()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}