using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

#if true


[CustomEditor(typeof(WFCGenerator)), Serializable]
public class WFCGenEditor : Editor
{
    public VisualTreeAsset m_InspectorXML;
    private VisualElement root;

    //Input variables
    private float gridSize;
    private int m_gridExtent;
    private Color lineColor;


    public override VisualElement CreateInspectorGUI()
    {
        root = new VisualElement();
        if (m_InspectorXML is null) throw new Exception("XML file for the Inspector missing");
        m_InspectorXML.CloneTree(root);

        var sizeFloatField = root.Q<FloatField>("m_gridSize");
        var extentFloatField = root.Q<IntegerField>("m_gridExtent");
        var lineColorField = root.Q<ColorField>("lineColor");

        sizeFloatField.BindProperty(serializedObject.FindProperty("m_gridSize"));
        sizeFloatField.BindProperty(serializedObject.FindProperty("m_gridSize"));
        lineColorField.BindProperty(serializedObject.FindProperty("lineColor"));


        sizeFloatField.RegisterValueChangedCallback(evt => { gridSize = sizeFloatField.value; });
        extentFloatField.RegisterValueChangedCallback(evt => { m_gridExtent = extentFloatField.value; });
        lineColorField.RegisterValueChangedCallback(evt => { lineColor = lineColorField.value; });
        return root;
    }

    private void OnSceneGUI()
    {
        Handles.color = lineColor;
        var lineCount = Mathf.RoundToInt((m_gridExtent * 2) / gridSize);
        if (lineCount % 2 == 0) lineCount++;
        var halfLineCount = lineCount / 2;
        for (var i = 0; i < lineCount; i++)
        {
            int intOffset = i - halfLineCount;
            float xCoord = intOffset * gridSize;
            float zCoord0 = halfLineCount * gridSize;
            float zCoord1 = -halfLineCount * gridSize;
            Vector3 p0 = new Vector3(xCoord, 0f, zCoord0);
            Vector3 p1 = new Vector3(xCoord, 0f, zCoord1);
            Handles.DrawAAPolyLine(p0, p1);
            p0 = new Vector3(zCoord0, 0f, xCoord);
            p1 = new Vector3(zCoord1, 0f, xCoord);
            Handles.DrawAAPolyLine(p0, p1);
        }
    }
}
#endif