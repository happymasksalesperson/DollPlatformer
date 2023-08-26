using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Transform spawnPoint;

    public GameObject objectToPool;
    public int initialPoolSize;

    private List<GameObject> pooledObjects = new List<GameObject>();

    public void ClearPool()
    {
        foreach (GameObject obj in pooledObjects)
        {
            Destroy(obj);
        }

        pooledObjects.Clear();
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

            IPooledObject pooledObj = objectToPool.GetComponent<IPooledObject>();
            pooledObj.owner = this;
            
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                obj.transform.position = spawnPoint.position;
                return obj;
            }
        }

        return null;
    }

    public void AddObjectToPool(GameObject obj)
    {
        obj.SetActive(false);

        if(!pooledObjects.Contains(obj))
        pooledObjects.Add(obj);
    }
}
