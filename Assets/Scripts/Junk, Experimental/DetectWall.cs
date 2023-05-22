using UnityEngine;

public class DetectWall : MonoBehaviour
{
    public LayerMask wallLayer;
    public float detectionRange = 1f;
    public Vector3 boxSize = new Vector3(5, 5, 5);

    private int needleAttackPower = -1;
    
    private void Update()
    {
        DetectWalls();
    }

    private void DetectWalls()
    {
        Collider[] colliders = new Collider[10]; // Adjust the size as needed
        int numColliders = Physics.OverlapBoxNonAlloc(transform.position, boxSize / 2f, colliders, Quaternion.identity);

        for (int i = 0; i < numColliders; i++)
        {
            Collider collider = colliders[i];

            ITakeDamage damageable = collider.GetComponent<ITakeDamage>();
            if (damageable != null)
            {
                Debug.Log("Hit "+collider.gameObject+" for " + needleAttackPower);

                damageable.ChangeHP(needleAttackPower);
            }
        }
    }
}