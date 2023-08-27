using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameWheel : MonoBehaviour
{
    public Transform midpoint;

    public ObjectPool objectPool;

    public GameObject prefabFireball;

    public int numFireballs;

    public List<GameObject> listFireballs;

    public float timeBetweenSpawn;

    public float circleSize;

    public float distanceBetweenObjects;

    private IEnumerator spawningFireballs;

    //you can save ienumerators as variables to target them with start/stop
    void Start()
    {
        spawningFireballs = SpawnFireballs();

        ChangeFireballNumber();
    }

    //spawns fireball prefab thru object pool
    public void ChangeFireballNumber()
    {
        if (spawningFireballs != null)
            StopCoroutine(spawningFireballs);

        listFireballs.Clear();

        objectPool.ClearPool();
        objectPool.SetPoolSizeAndCreate(prefabFireball, numFireballs);

        spawningFireballs = SpawnFireballs();
        StartCoroutine(spawningFireballs);
    }

    private IEnumerator SpawnFireballs()
    {
        int i = 0;
        while (i < numFireballs)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);

            GameObject fireball = objectPool.GetPooledObject();
            listFireballs.Add(fireball);
            SpreadObjectsInCircle();
            fireball.transform.SetParent(midpoint);
            i++;
        }
    }

    private IEnumerator DecreaseFireballs()
    {
        int i = numFireballs;
        while (0 < numFireballs)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);

            GameObject fireballToRemove = listFireballs[0];

            objectPool.AddObjectBackToPool(fireballToRemove);

            listFireballs.RemoveAt(0);
        }
    }

    void SpreadObjectsInCircle()
    {
        int numberOfObjects = listFireballs.Count;

        float angleStep = 360.0f / numberOfObjects;

        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;

            // Calculate the local position relative to the midpoint's local space.
            Vector3 localPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f) * circleSize;

            // Move the local position along the local forward axis.
            localPosition += Vector3.forward * i * distanceBetweenObjects;

            GameObject obj = listFireballs[i];

            // Convert the local position to world space and set it as the object's position.
            obj.transform.position = midpoint.TransformPoint(localPosition);
        }
    }
}