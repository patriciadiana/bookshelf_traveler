using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public string currentScene;

    public FantasySaveData fantasyData;
    public CrimeSaveData crimeData;
}

[System.Serializable]
public class FantasySaveData
{
    public Vector3 playerPosition;
    public string cameraBoundryName;
    public List<string> itemIds = new List<string>();
}

[System.Serializable]
public class CrimeSaveData
{
    public Vector3 playerPosition;
    public string cameraBoundryName;

    public bool isInSuspectMode;
}