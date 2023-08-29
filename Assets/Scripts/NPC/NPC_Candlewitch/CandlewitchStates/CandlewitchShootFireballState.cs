using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Candlewitch
{
    public class CandlewitchShootFireballState : CandlewitchStateBase
    {
        public FlameWheel flameWheel;

        public SpinTransform spinner;

        public float timeBetweenShots;

        public float vulnerableTime;

        public int numberOfFireballs;

        public int phase01Fireballs;
        public int phase02Fireballs;
        public int phase03Fireballs;

        public void OnEnable()
        {
            SetFireballs();
            void SetFireballs()
            {
                switch (brain.currentPhase)
                {
                    case (CandlewitchBrain.CandlewitchStateEnum.Phase01):
                        numberOfFireballs = phase01Fireballs;
                        break;
                    case (CandlewitchBrain.CandlewitchStateEnum.Phase02):
                        numberOfFireballs = phase02Fireballs;
                        break;

                    case (CandlewitchBrain.CandlewitchStateEnum.Phase03):
                        numberOfFireballs = phase03Fireballs;
                        break;
                }
            }

            flameWheel.numFireballs = numberOfFireballs;

            flameWheel.targetTransform = brain.playerTransform;

            spinner.ChangeDirection(brain.facingRight);

            flameWheel.ChangeFireballNumber();

            StartCoroutine(WaitToShoot());
        }

        private IEnumerator WaitToShoot()
        {
            while (!flameWheel.initialised)
                yield return null;

            StartCoroutine(Shoot());
        }

        private IEnumerator Shoot()
        {
            int i = 0;

            while (i < numberOfFireballs)
            {
                flameWheel.Shoot();

                yield return new WaitForSeconds(timeBetweenShots);

                i++;
            }

            StartCoroutine(Vulnerable());
        }

        private IEnumerator Vulnerable()
        {
            yield return new WaitForSeconds(vulnerableTime);
            
            if(!brain.testing)
            brain.ChangeState(CandlewitchBrain.CandlewitchStateEnum.Vanish);
        }

        public void OnDisable()
        {
        }
    }
}