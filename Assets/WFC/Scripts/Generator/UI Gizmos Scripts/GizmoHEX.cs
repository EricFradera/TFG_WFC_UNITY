using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoHEX : IGizmos
{
    private GameObject plane;
    private Material gridMat;
    private GameObject parentGizmo;
    private float tileOffsetX = 0.90f;
    private float tileOffsetZ = 0.95f;
    
    public void enableGizmo(Component component)
    {
        plane ??= GameObject.CreatePrimitive(PrimitiveType.Plane);
        gridMat ??= new Material(Shader.Find("Shader Graphs/hex"));
        parentGizmo ??= new GameObject("Gizmos");
        plane.transform.position = new Vector3(0, 0, 0);
        parentGizmo.transform.position = new Vector3(0, 0, 0);
        plane.transform.parent = parentGizmo.transform;
        parentGizmo.transform.parent = component.transform;
        if (plane.TryGetComponent<Renderer>(out var renderer)) renderer.material = gridMat;
    }

    

    public void generateGizmo(Color lineColor, float gridSize, float gridExtent)
    {
        var size = gridExtent / 10;
        
        plane.transform.localScale = new Vector3((size * tileOffsetX * 2), 1, size * tileOffsetZ*2);
        plane.transform.position = new Vector3(gridExtent/2+ (1+tileOffsetX)*0.85f, 0, gridExtent/2+ (1+tileOffsetZ*1.4f));
        gridMat.SetColor("_Color", lineColor);
        gridMat.SetFloat("_gridSize", gridExtent);
    }

    public void destroyGizmo()
    {
        Object.DestroyImmediate(parentGizmo);
        Object.DestroyImmediate(plane);
        Object.DestroyImmediate(gridMat);
    }
}