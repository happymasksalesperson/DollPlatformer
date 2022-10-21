using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollEventManager : MonoBehaviour
{
    //event for picking up the gun
    //arms the player and allows shooting/reloading
    public delegate void DollSafe();
    public static event DollSafe DollSafeEvent;
    public static void DollSafeFunction()
    {
        if(DollSafeEvent!=null)
        {
            DollSafeEvent();
        }
    }
}
