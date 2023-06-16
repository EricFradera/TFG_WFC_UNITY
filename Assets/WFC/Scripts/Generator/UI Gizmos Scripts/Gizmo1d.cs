using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo1d : IGizmos
{
    private GameObject quadPlane;
    private Material lineMaterial;
    private GameObject parentGizmo;

    public void enableGizmo(Component component)
    {
        quadPlane ??= GameObject.CreatePrimitive(PrimitiveType.Plane);
        lineMaterial ??= new Material(Shader.Find("Shader Graphs/1dGrid"));
        parentGizmo ??= new GameObject("Gizmos");

        quadPlane.transform.position = new Vector3(0, 0, 0);
        parentGizmo.transform.position = new Vector3(0, 0, 0);
        quadPlane.transform.parent = parentGizmo.transform;
        parentGizmo.transform.parent = component.transform;
        
        
        if (quadPlane.TryGetComponent<Renderer>(out var renderer)) renderer.material = lineMaterial;
    }

    public void generateGizmo(Color lineColor, float gridSize, float gridExtent)
    {
        var size = gridExtent / 10;
        quadPlane.transform.localScale = new Vector3(size, 1, size/4);
        lineMaterial.SetColor("_Color", lineColor);
        lineMaterial.SetFloat("_gridSize", Mathf.RoundToInt(gridSize));
    }

    public void destroyGizmo()
    {
        Object.DestroyImmediate(parentGizmo);
        Object.DestroyImmediate(quadPlane);
        Object.DestroyImmediate(lineMaterial);
    }
}