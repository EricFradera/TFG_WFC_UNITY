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
                
                current.populateList();
                wfcTilesList = configFile.wfcTilesList;
                
            }
            else
            {
                configFile = null;
                current.clearList();
                wfcTilesList = null;
            }
        });
        //Buttons
        generateButton.RegisterCallback<MouseUpEvent>((evt) => current.Generate());
        clearButton.RegisterCallback<MouseUpEvent>((evt) => current.ClearPreviousIteration());

        return root;
    }

    public void OnDisable()
    {
        if (configFile is null) return;
        gizmoList[(int)indexGizmo].destroyGizmo();
    }

    private void OnSceneGUI()
    {
        //TODO
        Func<VisualElement> makeItem = () =>
        {
            var tileItem = new VisualElement();
            tileItem.Add(new TextField());
            tileItem.Add(new Label());
            return tileItem;
        };
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            ((TextField)e.ElementAt(0)).value = wfcTilesList[i].tileName;
            ((TextField)e.ElementAt(0)).RegisterValueChangedCallback(evt =>
            {
                wfcTilesList[i].tileName = ((TextField)e.ElementAt(0)).text;
            });
            ((Label)e.ElementAt(1)).text = wfcTilesList[i].tileId;
        };
        listViewComponent.makeItem = makeItem;
        listViewComponent.bindItem = bindItem;
        listViewComponent.itemsSource = wfcTilesList;
        listViewComponent.selectionType = SelectionType.Multiple;
        listViewComponent.style.flexGrow = 1;
        listViewComponent.style.minHeight = 20;
        
        
        if (configFile is null) return;
        gizmoList[(int)indexGizmo].enableGizmo(current.transform);
        gizmoList[(int)indexGizmo].generateGizmo(lineColor, gridSize, gridExtent);
    }
}
#endif