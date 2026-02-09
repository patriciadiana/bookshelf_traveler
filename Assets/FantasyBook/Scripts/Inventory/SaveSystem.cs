using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    private string saveFilePath;
    private float autosaveInterval = 10f;
    private Coroutine autoSaveCoroutine;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");
    }

    private void Start()
    {
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
            SaveInventory();
        }
    }

    public void SaveInventory()
    {
        try
        {
            InventoryComponent inventory = FindFirstObjectByType<InventoryComponent>();
            InventorySaveData saveData = new InventorySaveData();

            foreach(ItemComponent item in inventory.items)
            {
                if (item != null && !string.IsNullOrEmpty(item.itemId))
                {
                    saveData.itemIds.Add(item.itemId);
                }
            }

            string jsonData = JsonUtility.ToJson(saveData, true);

            File.WriteAllText(saveFilePath, jsonData);

            Debug.Log("Inventory saved successfully! Items: " + saveData.itemIds.Count);

        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save inventory: " + e.Message);
        }
    }
}
