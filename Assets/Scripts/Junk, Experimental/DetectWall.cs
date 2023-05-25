using UnityEngine;

public class DetectWall : MonoBehaviour
{
    public LayerMask wallLayer;
    public float detectionRange = 1f;
    public Vector3 boxSize = new Vector3(5, 5, 5);

    public float ledgeDetectionRange = 50f;

    private int needleAttackPower = -1;

    private void Update()
    {
        DetectWalls();
    }

    private void DetectWalls()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.right, out hit, ledgeDetectionRange))
        {
            if (hit.collider.gameObject.layer == wallLayer)
            {
                Debug.Log("Hit wall");
            }
        }

       // Color rayColor = hit.collider != null && hit.collider.gameObject.layer==wallLayer ? Color.red : Color.green;
        //Debug.DrawRay(transform.position, transform.right * ledgeDetectionRange, rayColor);
    }
}