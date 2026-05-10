using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Sprite[] introSlides;
    public float slideDuration = 2f;
    public string nextSceneName = "_1FantasyBook";

    public GameObject optionsMenu;

    private void Start()
    {
        SoundManager.PlayMusic(MusicType.TITLE_THEME);
    }

    public void OnPlayButton()
    {
        string sceneToLoad = "_1FantasyBook";
        bool showCutscene = true;

        if (SaveSystem.Instance != null)
        {
            GameSaveData save = SaveSystem.Instance.GetSaveData();

            if (save != null && !string.IsNullOrEmpty(save.currentScene))
            {
                sceneToLoad = save.currentScene;
                showCutscene = false;
            }
        }

        if (showCutscene)
        {
            CutsceneData.slides = introSlides;
            CutsceneData.slideDuration = slideDuration;
            CutsceneData.nextScene = nextSceneName;

            SceneManager.LoadScene("Cutscene");
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnEnable()
    {
        if (SaveSystem.Instance != null)
            SaveSystem.Instance.allowSaving = true;
    }

    public void OnOptionsButton()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}