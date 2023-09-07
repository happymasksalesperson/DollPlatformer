using System;
using System.Collections;
using System.Collections.Generic;
using Candlewitch;
using UnityEngine;

public class candlewitchdeathstate : CandlewitchStateBase
{
    private void OnEnable()
    {
        modelView.OnFadeInFadeOut(true, brain.fadeTime);
        transform.position = brain.teleportPositions[0].transform.position;
    }
}
