using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrappleType
{
    zipline,
    swing
}

public class GrapplePoint : MonoBehaviour
{
    public GrappleType pointType;
    /*
     Needs to visibly change when the player is able to interact with it
     Most functionality should actually go on the player
     */
}