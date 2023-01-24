using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleView : MonoBehaviour
{
    public CollisionManager grappleRange;

    private void OnEnable()
    {
        grappleRange.OnTriggerEnterEvent += ChangeView;
    }

    void ChangeView(Collider obj)
    {
        //Open up or change colour to indicate interactibility
    }
}