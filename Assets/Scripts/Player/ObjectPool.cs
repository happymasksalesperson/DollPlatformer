using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectToPool;
    public int initialPoolSize = 10;

    private List<GameObject> pooledObjects = new List<GameObject>();

    private void Awake()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(objectToPool);

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
