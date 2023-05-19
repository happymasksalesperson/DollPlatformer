using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform targetTransform;
    public float yOffset = 2f;
    public float smoothSpeed = 0.5f;

    private Vector3 targetPosition;

    public bool targeting = false;

    public void Awake()
    {
        
    }

    public void SetCameraTransform(Transform newTransform)
    {
        targetTransform = newTransform;
        targeting = true;
    }

    void LateUpdate()
    {
        if (targeting && targetTransform!=null)
        {
            targetPosition = new Vector3(targetTransform.position.x, transform.position.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.smoothDeltaTime);
        }
    }

    
    public void ChangeVerticalPosition(float amount)
    {
        targetPosition.y += amount;
    }
}
