using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class Elevator_Model : MonoBehaviour
    {
        bool ElevatorGoingUp;

        public event Action ElevatorGoUpEvent;

        public void Up()
        {
            if (!ElevatorGoingUp)
            {
                ElevatorGoUpEvent?.Invoke();
                ElevatorGoingUp = true;
            }
        }


        public event Action ElevatorGoDownEvent;

        public void Down()
        {
            if (ElevatorGoingUp)
            {
                ElevatorGoDownEvent?.Invoke();
                ElevatorGoingUp = false;
            }
        }
    }
}