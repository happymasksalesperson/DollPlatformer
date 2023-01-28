using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager { get; private set; }
    public NPCSFX SFX { get; private set; }

    //public EXAMPLESCRIPT ExampleRef { get; private set; }

    public List<Transform> spawnPoints = new List<Transform>();

    public List<GameObject> NPC = new List<GameObject>();
    
    public List<Transform> itemPoints = new List<Transform>();

    public List<GameObject> item = new List<GameObject>();

    private void Awake()
    {
        if (levelManager != null && levelManager != this)
        {
            Destroy(this);
            return;
        }

        levelManager = this;

        SFX = GetComponentInChildren<NPCSFX>();

        SpawnNPC();
        SpawnItem();
    }
    
    private void SpawnNPC()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            int prefabIndex = i % NPC.Count;
            Instantiate(NPC[prefabIndex], spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }

    private void SpawnItem()
    {
        for (int i = 0; i < itemPoints.Count; i++)
        {
            int prefabIndex = i % item.Count;
            Instantiate(item[prefabIndex], itemPoints[i].position, itemPoints[i].rotation);
        }
    }
}