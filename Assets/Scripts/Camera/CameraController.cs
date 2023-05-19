using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    public FollowPlayer cam1FollowPlayer;

    public Transform cam1TargetTransform;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetCam1Target()
    {
        cam1FollowPlayer.SetCameraTransform(cam1TargetTransform);
    }
}
