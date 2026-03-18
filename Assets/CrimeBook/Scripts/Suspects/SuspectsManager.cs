using System.Collections;
using UnityEngine;

public class SuspectsManager : Singleton<SuspectsManager>
{
    private int interactedSuspects = 0;
    public int totalSuspects = 5;

    [SerializeField] private GameObject suspectsPanel;

    private bool accusationPhase = false;

    public void RegisterInteraction()
    {
        interactedSuspects++;

        if (interactedSuspects >= totalSuspects)
        {
            StartAccusationPhase();
        }
    }

    void StartAccusationPhase()
    {
        accusationPhase = true;

        PanelEvents.OnOpenPanel?.Invoke(
            suspectsPanel,
            "You talked to everyone. Choose the guilty suspect."
        );

        StartCoroutine(ClosePopUp());
    }

    public bool IsAccusationPhase()
    {
        return accusationPhase;
    }

    public void MakeAccusation(SuspectInteraction suspect)
    {
        if (!accusationPhase) return;

        if (suspect.isGuilty)
        {
            PanelEvents.OnOpenPanel?.Invoke(
                suspectsPanel,
                "Correct! You found the guilty suspect."
            );

            accusationPhase = false;
        }
        else
        {
            PanelEvents.OnOpenPanel?.Invoke(
                suspectsPanel,
                "Wrong suspect! Someone innocent was sent to jail"
            );
        }
    }

    private IEnumerator ClosePopUp()
    {
        yield return new WaitForSeconds(2f);
        PanelEvents.OnClosePanel?.Invoke();
    }
}