using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Candlewitch
{
    public class CandlewitchShootFireballState : CandlewitchStateBase
    {
        public Transform targetTransform;

        public Transform shootTransform;

        public ObjectPool pool;

        public GameObject fireball;

        public float shootForce;

        public void OnEnable()
        {
            targetTransform = brain.playerTransform;
            ShootFireball();
        }

        public void ShootFireball()
        {
            fireball = pool.GetPooledObject();
            fireball.transform.position = shootTransform.position;
            Rigidbody rb = fireball.GetComponent<Rigidbody>();

            Vector3 directionToTarget = (targetTransform.position - shootTransform.position).normalized;

            rb.AddForce(directionToTarget * shootForce, ForceMode.VelocityChange);

            StartCoroutine(Vanish());
        }

        private IEnumerator Vanish()
        {
            yield return new WaitForSeconds(brain.fadeTime * 2);

            brain.stateManager.ChangeState(brain.vanishState);
        }
    }
}