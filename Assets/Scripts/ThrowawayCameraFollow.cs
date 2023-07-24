using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowawayCameraFollow : MonoBehaviour
{
        public Transform playerTransform;
        public float smoothSpeed = 0.125f;
        public Vector3 offset;

        private void FixedUpdate()
        {
            Vector3 targetPosition = transform.position;
            targetPosition.x = playerTransform.position.x;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
}
