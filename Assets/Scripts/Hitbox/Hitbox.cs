using UnityEngine;

public class HitboxData
{
    public BoxCollider boxCollider;

    public HitboxData(Vector3 center, Vector3 halfExtents)
    {
        boxCollider = CreateBoxCollider(center, halfExtents);
    }

    private BoxCollider CreateBoxCollider(Vector3 center, Vector3 halfExtents)
    {
        GameObject hitboxObject = new GameObject("Hitbox");

        BoxCollider collider = hitboxObject.AddComponent<BoxCollider>();
        collider.center = center;
        collider.size = halfExtents * 2f;
        collider.isTrigger = true;
        
        return collider;
    }
}