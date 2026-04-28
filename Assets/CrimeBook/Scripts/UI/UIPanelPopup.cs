using TMPro;
using UnityEngine;

public class UIPanelPopup : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject UIPanel;
    public bool isOpen = false;
    [SerializeField] private TextMeshProUGUI panelText;

    private void OnEnable()
    {
        PanelEvents.OnOpenPanel += HandlePanelOpen;
        PanelEvents.OnClosePanel += CloseBoard;
    }

    private void OnDisable()
    {
        PanelEvents.OnOpenPanel -= HandlePanelOpen;
        PanelEvents.OnClosePanel -= CloseBoard;
    }

    private void HandlePanelOpen(GameObject panel, string message)
    {
        if (panel != null)
        {
            UIPanel = panel;
            panelText = UIPanel.GetComponentInChildren<TextMeshProUGUI>(true);
        }

        SetText(message);
        OpenBoard();
    }

    private void Start()
    {
        if (UIPanel != null)
            UIPanel.SetActive(false);
    }

    public bool CanInteract()
    {
        return !isOpen;
    }

    public void Interact()
    {
        OpenBoard();
    }

    public void OpenBoard()
    {
        if (UIPanel != null)
            UIPanel.SetActive(true);
        isOpen = true;
        InteractionState.IsUIOpen = true;
    }

    public void CloseBoard()
    {
        if (UIPanel != null)
            UIPanel.SetActive(false);
        isOpen = false;
        InteractionState.IsUIOpen = false;
    }

    public bool IsOpen() => isOpen;

    public void SetText(string message)
    {
        if (panelText != null)
        {
            panelText.text = message;
        }
    }
}