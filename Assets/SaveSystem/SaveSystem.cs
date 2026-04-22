using System;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    private string saveFilePath;
    private float autosaveInterval = 5f;
    private Coroutine autoSaveCoroutine;
    public bool allowSaving = true;

    public GameObject player;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saving.json");
        Debug.Log(saveFilePath);
    }

    private void Start()
    {
        LoadData();
        autoSaveCoroutine = StartCoroutine(AutoSave());
    }

    private void OnDestroy()
    {
        if (autoSaveCoroutine != null)
        {
            StopCoroutine(autoSaveCoroutine);
        }
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autosaveInterval);

            if (!allowSaving)
                continue;

            SaveData();
        }
    }

    public void SaveData()
    {
        if (!allowSaving)
        {
            Debug.Log("Save blocked (cutscene or loading)");
            return;
        }

        try
        {
            GameSaveData saveData = new GameSaveData();

            saveData.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            var savables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<ISaveable>();

            foreach (var savable in savables)
            {
                if (savable == null)
                    continue;

                try
                {
                    savable.SaveData(saveData);
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Failed to save {savable}: {e.Message}");
                }
                
            }

            string jsonData = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(saveFilePath, jsonData);

            Debug.Log("Data saved");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save inventory: " + e.Message);
        }
    }

    public void LoadData()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.Log("No save file found");
            return;
        }

        try
        {
            string jsonData = File.ReadAllText(saveFilePath);
            GameSaveData saveData = JsonUtility.FromJson<GameSaveData>(jsonData);

            var savables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<ISaveable>();

            foreach (var savable in savables)
            {
                if (savable == null) continue;

                try
                {
                    savable.LoadData(saveData);
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Failed to load {savable}: {e.Message}");
                }
            }

            Debug.Log("Data loaded");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load inventory: " + e.Message);
        }
    }

    public GameSaveData GetSaveData()
    {
        if (!File.Exists(saveFilePath))
            return null;

        try
        {
            string jsonData = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<GameSaveData>(jsonData);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to read save: " + e.Message);
            return null;
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted");
        }
        else
        {
            Debug.Log("No save file to delete");
        }
    }
}
