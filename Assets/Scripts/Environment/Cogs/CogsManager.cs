using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogsManager : MonoBehaviour
{
    //populate cogsList with Cogs to control
    //
    //TODO: get Doll actually walking on Cogs. Get Cogs physically rotating each other. Sort rotateLeft & targetBool nonsense. 
    
    public List<Cogs> cogsList;

    [Header("Stop/Start all cogs")]
    public bool spinAllCogs;

    [Header("Reverse all cogs")] 
    public bool rotateLeft;

    [Header("Stop/Start/Reverse specific cog in cogsList w/targetBool")]
    public int targetCogIndex;

    private bool targetBool;

    public void Start()
    {
        foreach (Cogs managedCogs in cogsList)
        {
           // Cogs newCogs = managedCogs.GetComponent<Cogs>();
           Cogs newCogs = managedCogs;
            if (newCogs != null)
            {
                StopStartAllCogsEvent += newCogs.IsMoving;
                RotateAllCogsEvent += newCogs.ReverseRotation;
            }
        }
    }

    public void StopStartAllCogs()
    {
        spinAllCogs = !spinAllCogs;
        OnStopStartAllCogs(spinAllCogs);
    }
    
    public void ReverseAllCogs()
    {
        OnRotateAllCogs(rotateLeft);
    }
    
    public void StopStartSpecificCog()
    {
        targetBool = !targetBool;
        
        Cogs newCog = cogsList[targetCogIndex].GetComponent<Cogs>();

        if (newCog != null)
        {
            newCog.IsMoving(targetBool);
        }
    }

    public void ReverseSpecificCog()
    {
        targetBool = !targetBool;
        
        Cogs newCog = cogsList[targetCogIndex].GetComponent<Cogs>();

        if (newCog != null)
        {
            newCog.ReverseRotation(targetBool);
        }
    }

    public event Action<bool> StopStartAllCogsEvent;

    public void OnStopStartAllCogs(bool isMove)
    {
        StopStartAllCogsEvent?.Invoke(isMove);
    }
    
    public event Action<bool> RotateAllCogsEvent;

    public void OnRotateAllCogs(bool rotateLeft)
    {
        RotateAllCogsEvent?.Invoke(rotateLeft);
    }
}
