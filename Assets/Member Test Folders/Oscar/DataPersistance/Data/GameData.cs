using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPos;

    public GameData()
    {
        playerPos = Vector3.zero;
    }
}
