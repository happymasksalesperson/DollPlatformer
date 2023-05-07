using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public Elevator elevator;
    
    public GameObject door;
    public Collider doorCollider;
    
    private void OnTriggerEnter(Collider other)
    {
        door.gameObject.SetActive(true);
        doorCollider.enabled = true;
        
        if (elevator.isGoingUp)
        {
            elevator.ElevatorDown();
        }
        else
        {
            elevator.ElevatorUp();
        }
    }

    private void Update()
    {
        if (elevator.isFinished)
        {
            door.gameObject.SetActive(false);
            doorCollider.enabled = false;
        }
    }
}
