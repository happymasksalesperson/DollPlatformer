using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform parent;

    private void Update()
    {
        transform.position = parent.transform.position;
    }
}
