using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSummonState : MonoBehaviour
{
    public Transform fireballTransform;

    public void Update()
    {
        FaceUp();
    }

    void FaceUp()
    {
        // Align the local up vector with the world up vector.
        Vector3 worldUp = Vector3.up;
        Vector3 localUp = fireballTransform.parent.InverseTransformDirection(worldUp);

        // Set the local rotation to match the world up direction.
        fireballTransform.localRotation = Quaternion.FromToRotation(Vector3.up, localUp);
    }
}