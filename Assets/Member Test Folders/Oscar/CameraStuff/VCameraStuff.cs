using System.Collections;
using Cinemachine;
using UnityEngine;

public class VCameraStuff : MonoBehaviour
{
    public GameObject player;
    public static CinemachineVirtualCamera vcam; // Reference to the Cinemachine Virtual Camera

    private void Start()
    {
        // Find the Cinemachine Virtual Camera in the scene
        vcam = FindObjectOfType<CinemachineVirtualCamera>();

        // If you prefer to reference the camera by name, you can use the following code instead:
        // vcam = GameObject.Find("YourCameraName").GetComponent<CinemachineVirtualCamera>();

        if (vcam == null)
        {
            Debug.LogError("Cinemachine Virtual Camera not found in the scene.");
            return;
        }
        
        // Set the camera to follow the player initially
        vcam.Follow = player.transform;
    }

    // You can add more methods to control the camera from this script
    public void SetCameraTarget(Transform newTarget)
    {
        if (vcam != null)
        {
            vcam.Follow = newTarget;
        }
    }
}
