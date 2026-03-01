using System;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class SaveSystem : Singleton<SaveSystem>
{
    private string saveFilePath;
    private float autosaveInterval = 5f;
    private Coroutine autoSaveCoroutine;

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
            SaveData();
        }
    }

    public void SaveData()
    {
        try
        {
            GameSaveData saveData = new GameSaveData();

            saveData.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            var savables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<ISavable>();

            foreach (var savable in savables)
            {
                savable.SaveData(saveData);
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
                .OfType<ISavable>();

            foreach (var savable in savables)
            {
                savable.LoadData(saveData);
            }

            Debug.Log("Data loaded");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load inventory: " + e.Message);
        }
    }

}
