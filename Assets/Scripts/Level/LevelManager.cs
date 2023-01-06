using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager { get; private set; }
    public NPC02SFX SFX { get; private set; }
    
    //public EXAMPLESCRIPT ExampleRef { get; private set; }
    
    private void Awake()
    {
        if (levelManager != null && levelManager != this)
        {
            Destroy(this);
            return;
        }
        
        levelManager = this;
        
        SFX = GetComponentInChildren<NPC02SFX>();
    }
}
