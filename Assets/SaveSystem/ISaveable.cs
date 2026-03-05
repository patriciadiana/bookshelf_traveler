using UnityEngine;

public interface ISaveable
{
    void SaveData(GameSaveData saveData);
    void LoadData(GameSaveData saveData);
}
