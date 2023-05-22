using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private Vector2 sizeVec;
    private Color lineColor;
    public WFCConfig configFile;
    public WFCGenerator current;

    public List<WFCTile> wfcTilesList;
    [SerializeField] private VisualTreeAsset itemEditor;
    public ListView listViewComponent;

    //3d visualization
    private GameObject cube;
    private Material gridMat;

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
        var vectorInput = root.Q<Vector2Field>("vecSize");


        //Binding components
        sizeFloatField.BindProperty(serializedObject.FindProperty("m_gridSize"));
        extentFloatField.BindProperty(serializedObject.FindProperty("m_gridExtent"));
        lineColorField.BindProperty(serializedObject.FindProperty("lineColor"));
        wfcConfigFileField.BindProperty(serializedObject.FindProperty("WFCConfigFile"));
        listViewComponent.BindProperty(serializedObject.FindProperty("wfcTilesList"));
        vectorInput.BindProperty(serializedObject.FindProperty("vecSize"));


        //modify variable values
        sizeFloatField.RegisterValueChangedCallback(evt => { gridSize = sizeFloatField.value; });
        extentFloatField.RegisterValueChangedCallback(evt => { gridExtent = extentFloatField.value; });
        lineColorField.RegisterValueChangedCallback(evt => { lineColor = lineColorField.value; });
        vectorInput.RegisterValueChangedCallback(evt => { sizeVec = vectorInput.value; });

        //Generate ListView
        wfcConfigFileField.RegisterValueChangedCallback(evt =>
        {
            if (wfcConfigFileField.value is not null)
            {
                switch (wfcConfigFileField.value)
                {
                    case WFC2DConfig wfc2DConfig:
                        Destroy3DGizmo();
                        configFile = wfc2DConfig;
                        break;
                    case WFC3DConfig wfc3DConfig:
                        Destroy3DGizmo();
                        configFile = wfc3DConfig;
                        create3DGizmo();
                        break;
                }

                current.populateList();
                wfcTilesList = configFile.wfcTilesList;

                listViewComponent.makeItem = itemEditor.CloneTree;
            }
            else
            {
                Destroy3DGizmo();
                current.clearList();
                wfcTilesList = null;
            }
        });
        //Buttons
        generateButton.RegisterCallback<MouseUpEvent>((evt) => current.Generate());
        clearButton.RegisterCallback<MouseUpEvent>((evt) => current.ClearPreviousIteration());

        return root;
    }

    private void create3DGizmo()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0, 0);
        if (current != null) cube.transform.parent = current.transform;
        gridMat = new Material(Shader.Find("Shader Graphs/gridShader"));
        if (cube.TryGetComponent<Renderer>(out var renderer)) renderer.material = gridMat;
    }

    public void OnDisable()
    {
        Destroy3DGizmo();
    }

    private void Destroy3DGizmo()
    {
        DestroyImmediate(gridMat);
        DestroyImmediate(cube);
    }

    private void OnSceneGUI()
    {
        listViewComponent.makeItem = itemEditor.CloneTree;
        if (configFile is null) return;
        if (configFile.GetType() == typeof(WFC2DConfig)) GenGrid2D();
        else if (configFile.GetType() == typeof(WFC3DConfig)) GenGrid3D();
    }

    private void GenGrid2D()
    {
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

    private void GenGrid3D()
    {
        var size = Mathf.RoundToInt((gridExtent * 2) / gridSize);
        if (size % 2 == 0) size++;
        var finalSize = size * gridSize - gridSize;
        var matSize = 1 / gridSize;
        cube.transform.localScale = new Vector3(finalSize, finalSize, finalSize);
        gridMat.SetColor("_lineColor", lineColor);
        gridMat.SetFloat("_gridSize", matSize);
    }
}
#endif