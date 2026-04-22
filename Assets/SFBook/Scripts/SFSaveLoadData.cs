using UnityEngine;

public class SFSaveLoadData : MonoBehaviour, ISaveable
{
    public bool enteredBattleMode = false;

    public void LoadData(GameSaveData saveData)
    {
        if (saveData.sfData == null) return;
        enteredBattleMode = saveData.sfData.enteredBattleMode;
    }

    public void SaveData(GameSaveData saveData)
    {
        if (saveData.sfData == null)
            saveData.sfData = new SFSaveData();

        saveData.sfData.enteredBattleMode = enteredBattleMode;
    }
}
