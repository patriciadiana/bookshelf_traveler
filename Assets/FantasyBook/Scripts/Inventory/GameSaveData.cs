using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public Vector3 playerPosition;
    public string cameraBoundryName;
    public List<string> itemIds = new List<string>();
}
