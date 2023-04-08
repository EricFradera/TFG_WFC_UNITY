using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

#if false
[CustomEditor(typeof(WFCGenerator))]
public class WFCGeneratorCustomEditor : Editor
{
    public float gridSize = 1f;

    private SerializedObject so;
    private SerializedProperty propGridSize;

    public override void OnInspectorGUI()
    {
        so.Update();
        EditorGUILayout.PropertyField(propGridSize);
        so.ApplyModifiedProperties();
        GUILayout.Label("Decide the grid size");
        gridSize = EditorGUILayout.FloatField("size", gridSize);
    }

    private void OnEnable()
    {
        so = new SerializedObject(this);
        propGridSize = so.FindProperty("gridSize");
        Selection.selectionChanged += Repaint;
        SceneView.duringSceneGui += DuringSceneGUI;
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= Repaint;
        SceneView.duringSceneGui -= DuringSceneGUI;
    }

    private void DuringSceneGUI(SceneView sceneView)
    {
        const float gridExtent = 16;
        var lineCount = Mathf.RoundToInt((gridExtent * 2) / gridSize);
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