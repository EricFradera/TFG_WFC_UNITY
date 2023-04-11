using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using WFC;

#if true


[CustomEditor(typeof(WFCGenerator)), Serializable]
public class WFCGenEditor : Editor
{
    public VisualTreeAsset m_InspectorXML;
    private VisualElement root;

    //Input variables
    private float gridSize;
    private float gridExtent;
    private Color lineColor;
    public WFCConfig configFile;
    public WFCGenerator current;

    public List<WFCTile> wfcTilesList;
    [SerializeField] private VisualTreeAsset itemEditor;
    public ListView listViewComponent;

    public override VisualElement CreateInspectorGUI()
    {
        current = target as WFCGenerator;
        root = new VisualElement();
        if (m_InspectorXML is null) throw new Exception("XML file for the Inspector missing");
        m_InspectorXML.CloneTree(root);
        //Input components
        var sizeFloatField = root.Q<FloatField>("m_gridSize");
        var extentFloatField = root.Q<FloatField>("m_gridExtent");
        var lineColorField = root.Q<ColorField>("lineColor");
        var wfcConfigFileField = root.Q<ObjectField>("WFCConfigFile");
        listViewComponent = root.Q<ListView>("wfcTilesList");
        var generateButton = root.Q<Button>("generateButton");
        var clearButton = root.Q<Button>("clearButton");

        //Binding components
        sizeFloatField.BindProperty(serializedObject.FindProperty("m_gridSize"));
        extentFloatField.BindProperty(serializedObject.FindProperty("m_gridExtent"));
        lineColorField.BindProperty(serializedObject.FindProperty("lineColor"));
        wfcConfigFileField.BindProperty(serializedObject.FindProperty("WFCConfigFile"));
        listViewComponent.BindProperty(serializedObject.FindProperty("wfcTilesList"));


        //modify variable values
        sizeFloatField.RegisterValueChangedCallback(evt => { gridSize = sizeFloatField.value; });
        extentFloatField.RegisterValueChangedCallback(evt => { gridExtent = extentFloatField.value; });
        lineColorField.RegisterValueChangedCallback(evt => { lineColor = lineColorField.value; });
        //Generate ListView
        wfcConfigFileField.RegisterValueChangedCallback(evt =>
        {
            configFile = wfcConfigFileField.value as WFCConfig;
            if (configFile is not null)
            {
                current.populateList();
                wfcTilesList = configFile.wfcTilesList;
                listViewComponent.makeItem = itemEditor.CloneTree;
            }
            else
            {
                current.clearList();
                wfcTilesList = null;
            }
        });
        //Buttons
        generateButton.RegisterCallback<MouseUpEvent>((evt) => current.Generate());
        clearButton.RegisterCallback<MouseUpEvent>((evt) => current.ClearPreviousIteration());

        return root;
    }


    private void genListView(ListView listView)
    {
        listView.makeItem = () => { return new Label("hi"); };
    }


    private void OnSceneGUI()
    {
        listViewComponent.makeItem = itemEditor.CloneTree;
        Handles.color = lineColor;
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