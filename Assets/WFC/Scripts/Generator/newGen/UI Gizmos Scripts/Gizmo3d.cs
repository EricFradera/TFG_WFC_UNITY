using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class Gizmo3d : IGizmos
{
    private GameObject cube;
    private Material gridMat;

    public void enableGizmo(Component component)
    {
        cube ??= GameObject.CreatePrimitive(PrimitiveType.Cube);
        gridMat ??= new Material(Shader.Find("Shader Graphs/gridShader"));
        cube.transform.position = new Vector3(0, 0, 0);
        //cube.transform.parent = component.transform;
        if (cube.TryGetComponent<Renderer>(out var renderer)) renderer.material = gridMat;
    }


    public void generateGizmo(Color lineColor, float gridSize, float gridExtent)
    {
        var size = Mathf.RoundToInt((gridExtent * 2) / gridSize);
        if (size % 2 == 0) size++;
        var finalSize = size * gridSize - gridSize;
        var matSize = 1 / gridSize;
        cube.transform.localScale = new Vector3(finalSize, 0, finalSize);
        gridMat.SetColor("_lineColor", lineColor);
        gridMat.SetFloat("_gridSize", matSize);
    }

    public void destroyGizmo()
    {
        Object.DestroyImmediate(cube);
        Object.DestroyImmediate(gridMat);
    }
}