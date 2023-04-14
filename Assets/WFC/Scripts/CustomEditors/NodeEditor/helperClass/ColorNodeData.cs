using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColorNodeData : ScriptableObject
{
    public Color nodeColor;
    public string uid;
    public nodeData nodeData;
    private void generateUID() => uid = GUID.Generate().ToString();
}