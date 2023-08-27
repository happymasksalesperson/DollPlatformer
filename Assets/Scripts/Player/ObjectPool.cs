using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject objectToPool;
    public int initialPoolSize;

    public Dictionary<PooledObject, GameObject> poolDictionary = new Dictionary<PooledObject, GameObject>();

    public void ClearPool()
    {
        foreach (var keyValuePair in poolDictionary)
        {
            GameObject obj = keyValuePair.Value;
            Destroy(obj);
        }

        poolDictionary.Clear();
    }

    public void SetPoolSizeAndCreate(GameObject poolObj, int poolSize)
    {
        objectToPool = poolObj;
        initialPoolSize = poolSize;
        CreatePool();
    }

    public void CreatePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.transform.position = spawnPoint.position;

            PooledObject pooledObj = obj.GetComponent<PooledObject>();
            pooledObj.owner = this;
            pooledObj.spawnPosition = spawnPoint;
            pooledObj.ChangeActive(false);

            poolDictionary[pooledObj] = obj;
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (var keyValuePair in poolDictionary)
        {
            PooledObject poolObj = keyValuePair.Key;
            GameObject obj = keyValuePair.Value;

            if (!obj.activeInHierarchy)
            {
                poolObj.ChangeActive(true);
                obj.transform.position = spawnPoint.position;
                return obj;
            }
        }

        return null;
    }

    public void AddObjectBackToPool(GameObject obj)
    {
        PooledObject poolObj = obj.GetComponent<PooledObject>();
        if (poolObj != null)
        {
            poolDictionary[poolObj] = obj;
        }
    }
}