using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSave : MonoBehaviour, ISaveable
{
    public void LoadData(GameSaveData saveData)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "_1FantasyBook")
        {
            if (saveData.fantasyData == null) return;

            transform.position = saveData.fantasyData.playerPosition;
        }
        else if (sceneName == "_3CrimeBook")
        {
            if(saveData.crimeData == null) return;

            transform.position = saveData.crimeData.playerPosition;

            if (saveData.crimeData.isInSuspectMode)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void SaveData(GameSaveData saveData)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "_1FantasyBook")
        {
            if (saveData.fantasyData == null)
                saveData.fantasyData = new FantasySaveData();

            saveData.fantasyData.playerPosition = transform.position;
        }
        else if (sceneName == "_3CrimeBook")
        {
            if (saveData.crimeData == null)
                saveData.crimeData = new CrimeSaveData();

            saveData.crimeData.playerPosition = transform.position;
        }
    }
}