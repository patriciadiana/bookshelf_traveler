using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    private string saveFilePath;
    private float autosaveInterval = 5f;
    private Coroutine autoSaveCoroutine;

    public GameObject player;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");
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
            InventoryComponent inventory = FindFirstObjectByType<InventoryComponent>();
            if (inventory == null)
            {
                Debug.LogWarning("No InventoryComponent found to save");
                return;
            }

            GameSaveData saveData = new GameSaveData();

            /* Save player position */
            player = GameObject.FindGameObjectWithTag("Player");
            saveData.playerPosition = player.transform.position;

            /* Save inventory */
            foreach (ItemData item in inventory.items)
            {
                if (item != null && !string.IsNullOrEmpty(item.itemId))
                {
                    saveData.itemIds.Add(item.itemId);
                }
            }

            /* Save camera bound name */
            CameraFollow cameraFollow = FindFirstObjectByType<CameraFollow>();
            if (cameraFollow != null && cameraFollow.cameraBoundsCollider != null)
            {
                saveData.cameraBoundryName = cameraFollow.cameraBoundsCollider.gameObject.name;
            }

            string jsonData = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(saveFilePath, jsonData);
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

            /* Restore inventory */
            InventoryComponent inventory = FindFirstObjectByType<InventoryComponent>();
            ItemDatabase database = FindFirstObjectByType<ItemDatabase>();

            if (inventory == null || database == null)
            {
                Debug.LogError("InventoryComponent or ItemDatabase missing");
                return;
            }

            inventory.items.Clear();

            foreach (string itemId in saveData.itemIds)
            {
                ItemData item = database.GetItemById(itemId);

                if (item != null && inventory.items.Count < inventory.capacity)
                {
                    inventory.items.Add(item);
                }
                else
                {
                    Debug.LogWarning($"Item not found or inventory full: {itemId}");
                }
            }

            InventorySystem.Instance.inventoryUI.Refresh(inventory.items);

            /* Restore player position */
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = saveData.playerPosition;
            }

            /* Restore camera boundry */
            CameraFollow cameraFollow = FindFirstObjectByType<CameraFollow>();
            if (cameraFollow != null && !string.IsNullOrEmpty(saveData.cameraBoundryName))
            {
                GameObject boundaryObj = GameObject.Find(saveData.cameraBoundryName);
                if (boundaryObj != null)
                {
                    PolygonCollider2D collider = boundaryObj.GetComponent<PolygonCollider2D>();
                    if (collider != null)
                    {
                        cameraFollow.UpdateCameraBounds(collider);
                    }
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load inventory: " + e.Message);
        }
    }

}
