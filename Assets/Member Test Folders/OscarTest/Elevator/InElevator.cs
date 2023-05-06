using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InElevator : MonoBehaviour
{
    public Elevator elevator;

    public List<Collider> players;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IPlayer>() != null)
        {
            players.Add(other);
            other.transform.parent = elevator.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
        players.Remove(other);
    }
}
