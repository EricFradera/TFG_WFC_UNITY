using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public VisualTreeAsset itemList;
    private VisualElement root;

    //Input variables
    private float gridSize;
    private float gridExtent;
    private bool backtracking;
    private int tileSetIndex;
    private Color lineColor;
    public WFCConfig configFile;
    public WFCGenerator current;

    public List<SerializedObject> wfcTilesList = new();
    [SerializeField] private VisualTreeAsset itemEditor;
    public ListView listViewComponent;

    //Gizmos
    private List<IGizmos> gizmoList;
    private GizmoGenerator indexGizmo;


    public WFCGenEditor()
    {
        gizmoList = new List<IGizmos>()
        {
            new Gizmo1d(),
            new Gizmo2d(),
            new Gizmo3d(),
            new GizmoHEX()
        };
    }

    private enum GizmoGenerator
    {
        GIZMO1D,
        GIZMO2D,
        GIZMO3D,
        GIZMOHEX,
    }

    public override VisualElement CreateInspectorGUI()
    {
        Debug.Log("Called");
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
        var backtrackingToggle = root.Q<Toggle>("backtracking");
        var tileSetIndexInput = root.Q<IntegerField>("tileSetIndex");


        //Binding components
        sizeFloatField.BindProperty(serializedObject.FindProperty("m_gridSize"));
        extentFloatField.BindProperty(serializedObject.FindProperty("m_gridExtent"));
        lineColorField.BindProperty(serializedObject.FindProperty("lineColor"));
        //wfcConfigFileField.BindProperty(serializedObject.FindProperty("WFCConfigFile"));
        //listViewComponent.BindProperty(serializedObject.FindProperty("wfcTilesList"));
        vectorInput.BindProperty(serializedObject.FindProperty("vecSize"));
        backtrackingToggle.BindProperty(serializedObject.FindProperty("backtracking"));
        tileSetIndexInput.BindProperty(serializedObject.FindProperty("tileSetIndex"));


        //modify variable values
        sizeFloatField.RegisterValueChangedCallback(evt => { gridSize = sizeFloatField.value; });
        extentFloatField.RegisterValueChangedCallback(evt => { gridExtent = extentFloatField.value; });
        lineColorField.RegisterValueChangedCallback(evt => { lineColor = lineColorField.value; });
        backtrackingToggle.RegisterValueChangedCallback(evt => { backtracking = backtrackingToggle.value; });
        tileSetIndexInput.RegisterValueChangedCallback(evt => { tileSetIndex = tileSetIndexInput.value; });


        //Generate ListView
        wfcConfigFileField.RegisterValueChangedCallback(evt =>
        {
            if (wfcConfigFileField.value is not null)
            {
                var newConfig = wfcConfigFileField.value;
                switch (newConfig)
                {
                    case WFC1DConfig wfc1DConfig:
                        indexGizmo = GizmoGenerator.GIZMO1D;
                        configFile = wfc1DConfig;
                        break;
                    case WFC2DConfig wfc2DConfig:
                        indexGizmo = GizmoGenerator.GIZMO2D;
                        configFile = wfc2DConfig;
                        break;
                    case WFC3DConfig wfc3DConfig:
                        indexGizmo = GizmoGenerator.GIZMO3D;
                        configFile = wfc3DConfig;
                        break;
                    case WFCHexConfig wfcHexConfig:
                        indexGizmo = GizmoGenerator.GIZMOHEX;
                        configFile = wfcHexConfig;
                        break;
                }

                wfcTilesList.Clear();
                wfcTilesList.AddRange(configFile.wfcTilesList.Select(x => new SerializedObject(x)));
                current.populateList();
                listViewComponent.Rebuild();
            }
            else
            {
                configFile = null;
                current.clearList();
                wfcTilesList.Clear();
                listViewComponent.Rebuild();
            }
        });
        //Buttons
        generateButton.RegisterCallback<MouseUpEvent>((evt) => current.Generate());
        clearButton.RegisterCallback<MouseUpEvent>((evt) => current.ClearPreviousIteration());
        CreateListView();
        return root;
    }

    public void OnDisable()
    {
        if (configFile is null) return;
        gizmoList[(int)indexGizmo].destroyGizmo();
    }

    private void OnSceneGUI()
    {
       if (configFile is null) return;
        gizmoList[(int)indexGizmo].enableGizmo(current.transform);
        gizmoList[(int)indexGizmo].generateGizmo(lineColor, gridSize, gridExtent);
    }

    private void CreateListView()
    {
        VisualElement MakeItem() => itemList.CloneTree();
        
        void BindItem(VisualElement e, int i)
        {
            SerializedObject wfcTile = wfcTilesList[i];
            var textFieldInput = e.Q<TextField>("tileName");
            textFieldInput.BindProperty(wfcTile.FindProperty("tileName"));
        }

        listViewComponent.makeItem = MakeItem;
        listViewComponent.bindItem = BindItem;
        listViewComponent.itemsSource = wfcTilesList;
    }
}
#endif