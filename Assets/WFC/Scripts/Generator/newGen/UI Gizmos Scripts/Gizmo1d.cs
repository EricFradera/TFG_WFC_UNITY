using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo1d : IGizmos
{
    private GameObject plane;
    private Material lineMaterial;

    public void enableGizmo(Component component)
    {
        plane ??= GameObject.CreatePrimitive(PrimitiveType.Plane);
        lineMaterial ??= new Material(Shader.Find("Shader Graphs/1dGrid"));
        plane.transform.position = new Vector3(0, 0, 0);
        if (plane.TryGetComponent<Renderer>(out var renderer)) renderer.material = lineMaterial;
    }

    public void generateGizmo(Color lineColor, float gridSize, float gridExtent)
    {
        plane.transform.localScale = new Vector3(gridExtent, gridExtent, gridExtent);
        lineMaterial.SetColor("_Color", lineColor);
        lineMaterial.SetFloat("_gridSize", Mathf.RoundToInt(gridSize));
    }

    public void destroyGizmo()
    {
        Object.DestroyImmediate(plane);
        Object.DestroyImmediate(lineMaterial);
    }
}