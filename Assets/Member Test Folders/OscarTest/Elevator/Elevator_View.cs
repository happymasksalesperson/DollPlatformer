using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class Elevator_View : MonoBehaviour
{
    public Elevator_Model elevatorModel;

    private void OnEnable()
    {
        elevatorModel.ElevatorGoUpEvent += GoUp;
        elevatorModel.ElevatorGoDownEvent += GoDown;
    }

    void GoUp()
    {
        print(1);
    }

    void GoDown()
    {
        print(2);
    }
}