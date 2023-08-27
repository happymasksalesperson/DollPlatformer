using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Candlewitch
{
    public class CandlewitchShootFireballState : CandlewitchStateBase
    {
        public float timeBetweenAttacks;

        public FlameWheel flameWheel;

        public void OnEnable()
        {
            flameWheel.numFireballs = brain.numberOfFireballs;

            flameWheel.spinner.direction = brain.facingRight;

            flameWheel.StartSpinning();
        }

        public void OnDisable()
        {

        }
    }
}