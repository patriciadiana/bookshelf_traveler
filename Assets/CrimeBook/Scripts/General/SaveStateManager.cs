using UnityEngine;

public class SaveStateManager : MonoBehaviour, ISaveable
{
    public bool isInSuspectMode = false;

    public void SaveData(GameSaveData data)
    {
        if (data.crimeData == null)
            data.crimeData = new CrimeSaveData();

        data.crimeData.isInSuspectMode = isInSuspectMode;
    }

    public void LoadData(GameSaveData data)
    {
        if (data.crimeData == null) return;

        isInSuspectMode = data.crimeData.isInSuspectMode;
    }
}