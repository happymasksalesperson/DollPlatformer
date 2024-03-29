using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager { get; private set; }
    public NPCSFX SFX { get; private set; }

    //public EXAMPLESCRIPT ExampleRef { get; private set; }

    public Transform playerSpawn;

    public GameObject player; 
    
    [Header("POTENTIAL NPC PREFABS IN LEVEL")]
    public List<GameObject> NPC = new List<GameObject>();

    [Header("NPC SPAWN POINTS")]
    public List<Transform> NPCSpawnPoints = new List<Transform>();
    
    public List<GameObject> item = new List<GameObject>();

    public List<Transform> itemPoints = new List<Transform>();

    
    private void Awake()
    {
        if (levelManager != null && levelManager != this)
        {
            Destroy(this);
            return;
        }

        levelManager = this;

        SFX = GetComponentInChildren<NPCSFX>();
       // SpawnPlayer();
    }

    public GameObject SpawnPlayer()
    {
        GameObject playerInstance = Instantiate(player, playerSpawn.position, playerSpawn.rotation) as GameObject;
        playerSpawn.gameObject.SetActive(false);
        return playerInstance;
    }

    public void SpawnNPC()
    {
        if (NPCSpawnPoints.Any())
        {
            for (int i = 0; i < NPCSpawnPoints.Count; i++)
            {
                int prefabIndex = i % NPC.Count;
                Instantiate(NPC[prefabIndex], NPCSpawnPoints[i].position, NPCSpawnPoints[i].rotation);
            }
        }
    }

    public void SpawnItem()
    {
        if (itemPoints.Any())
        {
            for (int i = 0; i < itemPoints.Count; i++)
            {
                int prefabIndex = i % item.Count;
                Instantiate(item[prefabIndex], itemPoints[i].position, itemPoints[i].rotation);
            }
        }
    }
}