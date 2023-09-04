using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManipulation : MonoBehaviour
{
    public CinemachineVirtualCamera vCamera;

    public bool yAxisMovement;
    public bool xAxisMovement;

    public float waitToTransitionAmount;

    public float xAxisNotfollowed;
    public float yAxisNotfollowed;
    public float defautFollowingValue;


    private void Start()
    {
        vCamera.GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IPlayer>() != null)
        {
            if (yAxisMovement && xAxisMovement)
            {
                vCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = defautFollowingValue; 
                vCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = defautFollowingValue;
            }

            if (yAxisMovement && !xAxisMovement)
            {
                vCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = xAxisNotfollowed; 
                vCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = defautFollowingValue; 
            }

            if (!yAxisMovement && xAxisMovement)
            {
                vCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = defautFollowingValue;
                vCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = yAxisNotfollowed; 
            }

            if (!yAxisMovement && !xAxisMovement)
            {
                vCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = xAxisNotfollowed; 
                vCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = yAxisNotfollowed;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(moveSlowly());
    }

    public IEnumerator moveSlowly()
    {
        vCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 5f; 
        vCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 5f; 

        yield return new WaitForSeconds(waitToTransitionAmount);
        
        vCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = .5f; 
        vCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = .5f; 
    }
    
}
