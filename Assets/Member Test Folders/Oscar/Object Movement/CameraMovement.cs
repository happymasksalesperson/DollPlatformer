using UnityEngine;
using Oscar;

public class CameraMovement : MonoBehaviour
{
    public GameObject camera;
    
    public Transform zoomOut;
    public Transform zoomIn;
    public float moveSpeed;
    private Vector3 targetPosition;
    
    public bool isZooming;

    private void Start()
    {
        Elevator.ElevatorMoveEvent += ZoomCameraOut;
    }

    void FixedUpdate()
    {
        if (isZooming)
        {
            targetPosition = zoomOut.position;
        }
        else
        {
            targetPosition = zoomIn.position;
        }
        
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition);

        camera.transform.position = direction * (moveSpeed * distance * 2);

        //if its zooming out then zoom back in when it gets to the top
        if (isZooming)
        { 
            if (distance <= 1f)
            {
                camera.transform.position = zoomOut.position;
                ZoomCameraIn();
            }
        }
        else //if its zooming in then just lock. 
        {
            if (distance <= 1f)
            {
                camera.transform.position = zoomIn.position;
            }
        }
    }
    
    public void ZoomCameraOut()
    {
        isZooming = false;  
    }
    
    public void ZoomCameraIn()
    { 
        isZooming = true;
    }
}
