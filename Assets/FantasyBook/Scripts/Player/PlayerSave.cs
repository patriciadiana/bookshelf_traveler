using UnityEngine;

public class PlayerSave : MonoBehaviour, ISaveable
{
    public void LoadData(GameSaveData saveData)
    {
        if (saveData.fantasyData == null)
            return;

        transform.position = saveData.fantasyData.playerPosition;
    }

    public void SaveData(GameSaveData saveData)
    {
        if (saveData.fantasyData == null)
            saveData.fantasyData = new FantasySaveData();

        saveData.fantasyData.playerPosition = transform.position;
    }
}
