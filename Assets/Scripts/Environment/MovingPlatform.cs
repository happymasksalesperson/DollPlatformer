using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> platformPoints;
    public float moveSpeed;
    public int target;
    
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, platformPoints[target].position,moveSpeed * Time.deltaTime);
        
        if (transform.position == platformPoints[target].position)
        {
            if (target == platformPoints.Count - 1)
            {
                target = 0;
            }
            else
            {
                target++;
            }
        }
    }
}
