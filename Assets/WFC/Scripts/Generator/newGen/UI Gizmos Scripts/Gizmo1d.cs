using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo1d : IGizmos
{
    private GameObject quadPlane;
    private Material lineMaterial;

    public void enableGizmo(Component component)
    {
        quadPlane ??= GameObject.CreatePrimitive(PrimitiveType.Plane);
        lineMaterial ??= new Material(Shader.Find("Shader Graphs/1dGrid"));
        quadPlane.transform.position = new Vector3(0, 0, 0);
        //quadPlane.transform.Rotate(new Vector3(90, 0, 0));
        if (quadPlane.TryGetComponent<Renderer>(out var renderer)) renderer.material = lineMaterial;
    }

    public void generateGizmo(Color lineColor, float gridSize, float gridExtent)
    {
        var size = gridExtent / 10;
        quadPlane.transform.localScale = new Vector3(size, size, size);
        lineMaterial.SetColor("_Color", lineColor);
        lineMaterial.SetFloat("_gridSize", Mathf.RoundToInt(gridSize));
    }

    public void destroyGizmo()
    {
        Object.DestroyImmediate(quadPlane);
        Object.DestroyImmediate(lineMaterial);
    }
}