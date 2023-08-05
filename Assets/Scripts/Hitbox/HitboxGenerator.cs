using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitboxGenerator : MonoBehaviour
{
    public List<BoxCollider> hitboxes = new List<BoxCollider>();
    public List<BoxCollider> hurtboxes = new List<BoxCollider>();

    public void GenerateHitboxes(List<BoxData> hitboxData, List<BoxData> hurtboxData)
    {
        ClearHitboxes();

        foreach (var data in hitboxData)
        {
            GameObject hitboxObject = new GameObject("Hitbox");
            BoxCollider collider = hitboxObject.AddComponent<BoxCollider>();
            hitboxObject.transform.SetParent(transform);
            hitboxObject.transform.position = transform.position;

            collider.center = data.position;
            collider.size = data.size;
            collider.isTrigger = true;

            hitboxes.Add(collider);
        }

        foreach (var data in hurtboxData)
        {
            GameObject hurtboxObject = new GameObject("Hurtbox");
            BoxCollider collider = hurtboxObject.AddComponent<BoxCollider>();
            hurtboxObject.transform.SetParent(transform);

            collider.center = data.position;
            collider.size = data.size;
            collider.isTrigger = true;

            hurtboxes.Add(collider);
        }
    }

    public void ClearHitboxes()
    {
        foreach (var hitbox in hitboxes)
        {
            DestroyImmediate(hitbox.gameObject);
        }

        foreach (var hurtbox in hurtboxes)
        {
            DestroyImmediate(hurtbox.gameObject);
        }

        hitboxes.Clear();
        hurtboxes.Clear();
    }
}