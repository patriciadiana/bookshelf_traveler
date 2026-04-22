using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public string tag;
    public GameObject prefab;
    public int poolSize;
}

public class ObjectPool : Singleton<ObjectPool>
{
    public List<PoolItem> itemsToPool;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, GameObject> prefabDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        prefabDictionary = new Dictionary<string, GameObject>();

        foreach (PoolItem item in itemsToPool)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < item.poolSize; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(item.tag, objectPool);
            prefabDictionary.Add(item.tag, item.prefab);
        }
    }

    public GameObject GetObjectFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        Queue<GameObject> poolQueue = poolDictionary[tag];

        GameObject objectToGet;

        if (poolQueue.Count == 0)
        {
            GameObject prefab = prefabDictionary[tag];
            objectToGet = Instantiate(prefab);
        }
        else
        {
            objectToGet = poolQueue.Dequeue();
        }

        objectToGet.SetActive(true);
        return objectToGet;
    }

    public void ReturnObjectToPool(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}