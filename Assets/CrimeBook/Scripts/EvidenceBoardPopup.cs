using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EvidenceBoardPopup : MonoBehaviour, IInteractable
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

    private void OpenBoard()
    {
        evidenceUIPanel.SetActive(true);
        isOpen = true;

        Time.timeScale = 0f;
    }

    public void CloseBoard()
    {
        evidenceUIPanel.SetActive(false);
        isOpen = false;

        Time.timeScale = 1f;
    }
}
