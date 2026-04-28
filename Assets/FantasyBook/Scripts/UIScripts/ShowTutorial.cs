using System.Collections;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    public MoveCharacter player;
    public GameObject tutorialUI;

    private bool touchReceived = false;

    private void Start()
    {
        StartCoroutine(PlayTutorialWithDelay());
    }

    private IEnumerator PlayTutorialWithDelay()
    {
        yield return new WaitForSeconds(1f);

        if (InputManager.Instance != null)
            InputManager.Instance.OnStartTouch += OnTutorialTouch;
        else
            Debug.LogError("InputManager instance not found!");

        player.SetCanMove(false);
        tutorialUI.SetActive(true);

        yield return new WaitUntil(() => touchReceived);

        if (InputManager.Instance != null)
            InputManager.Instance.OnStartTouch -= OnTutorialTouch;

        tutorialUI.SetActive(false);
        player.SetCanMove(true);
    }

    private void OnTutorialTouch(Vector2 position, float time)
    {
        touchReceived = true;
    }
}