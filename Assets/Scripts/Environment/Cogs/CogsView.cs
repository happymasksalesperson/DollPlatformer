using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogsView : MonoBehaviour
{
    public Color currentColor;

    private Renderer[] renderers;
    private MaterialPropertyBlock materialPropertyBlock;

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
    }

    public void ChangeColor(Color newColor)
    {
        currentColor = newColor;
        materialPropertyBlock.SetColor("_Color", currentColor);

        foreach (Renderer renderer in renderers)
        {
            renderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}
