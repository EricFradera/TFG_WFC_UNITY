using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IGizmos
{
    public void enableGizmo(Component component);
    public void generateGizmo(Color lineColor, float gridSize, float gridExtent);
    public void destroyGizmo();
}