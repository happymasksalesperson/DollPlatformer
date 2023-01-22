using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager { get; private set; }
    public NPC02SFX SFX { get; private set; }

    //public EXAMPLESCRIPT ExampleRef { get; private set; }

    public List<Transform> spawnPoints = new List<Transform>();

    public List<GameObject> NPC = new List<GameObject>();

    private void Awake()
    {
        if (levelManager != null && levelManager != this)
        {
            Destroy(this);
            return;
        }

        levelManager = this;

        SFX = GetComponentInChildren<NPC02SFX>();

        SpawnNPC();
    }

    private void SpawnNPC()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Instantiate(NPC[i % NPC.Count], spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }
}