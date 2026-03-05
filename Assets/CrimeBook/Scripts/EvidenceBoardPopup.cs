using UnityEngine;

public class EvidenceBoardPopup : Singleton<EvidenceBoardPopup>, IInteractable
{
    [SerializeField] private GameObject evidenceUIPanel;
    private bool isOpen = false;

    private void Start()
    {
        evidenceUIPanel.SetActive(false);
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
        evidenceUIPanel.SetActive(true);
        isOpen = true;
    }

    public void CloseBoard()
    {
        evidenceUIPanel.SetActive(false);
        isOpen = false;
    }

    public bool IsOpen() => isOpen;
}