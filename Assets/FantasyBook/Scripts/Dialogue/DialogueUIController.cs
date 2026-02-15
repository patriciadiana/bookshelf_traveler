using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIController : Singleton<DialogueUIController>
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Image portraitImage;

    public Transform choiceContainer;
    public GameObject choiceButtonPrefab;

    public void ShowDialogueUI(bool show)
    {
        dialoguePanel.SetActive(show);
    }

    public void SetNPCInfo(string npcName, Sprite portrait)
    {
        nameText.text = npcName;
        portraitImage.sprite = portrait;
    }

    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }

    public void ClearChoices()
    {
        foreach (Transform child in choiceContainer)
            Destroy(child.gameObject);
    }

    public void CreateChoiceButton(string choiceText, UnityEngine.Events.UnityAction onClick)
    {
        Debug.Log("Creating button: " + choiceText);
        GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);
        choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choiceText;

        Button btn = choiceButton.GetComponent<Button>();
        btn.onClick.AddListener(() => {
            Debug.Log("Button clicked: " + choiceText);
            onClick?.Invoke();
        });
    }

}
