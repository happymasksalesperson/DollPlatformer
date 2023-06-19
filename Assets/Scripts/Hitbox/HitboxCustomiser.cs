using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class HitboxCustomiser : MonoBehaviour
{
    public List<BoxData> hitBoxes = new List<BoxData>();
    public List<BoxData> hurtBoxes = new List<BoxData>();

    public HitboxGenerator generator;

    public void OnEnable()
    {
        generator.GenerateHitboxes(hitBoxes, hurtBoxes);
    }

    private void OnDrawGizmos()
    {
        DrawColoredWireCubes(hitBoxes, Color.red);
        DrawColoredWireCubes(hurtBoxes, Color.green);
    }

    private void DrawColoredWireCubes(List<BoxData> boxes, Color color)
    {
        Color transparentColor = new Color(color.r, color.g, color.b, 0.5f);

        foreach (var box in boxes)
        {
            Gizmos.color = transparentColor;
            Gizmos.matrix = Matrix4x4.TRS(transform.position + box.position, transform.rotation, box.size);
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
        }
    }
}

[System.Serializable]
public class BoxData
{
    public Vector3 position;
    public Vector3 size = Vector3.one;
}