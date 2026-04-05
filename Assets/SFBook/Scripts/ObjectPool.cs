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

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

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
        }
    }

    public GameObject GetObjectFromPool(string tag, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag '" + tag + "' doesn't exist!");
            return null;
        }

        Queue<GameObject> poolQueue = poolDictionary[tag];

        GameObject objectToGet = poolQueue.Dequeue();

        objectToGet.SetActive(true);
        objectToGet.transform.position = position;

        poolQueue.Enqueue(objectToGet);

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
    }
}