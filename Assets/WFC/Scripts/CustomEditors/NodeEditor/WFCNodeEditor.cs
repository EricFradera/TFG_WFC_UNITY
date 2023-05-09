using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;
using WFC;

public class WFCNodeEditor : EditorWindow
{
    private WFCNodeEditorView wfcNodeEditorView;
    private InspectorView inspectorView;


    [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("WFC/WFCNodeEditor")]
    public static void OpenWindow()
    {
        WFCNodeEditor wnd = GetWindow<WFCNodeEditor>();
        wnd.titleContent = new GUIContent("WFCNodeEditor");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceId, int line)
    {
        if (Selection.activeObject is not WFCConfig) return false;
        OpenWindow();
        return true;

    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        m_VisualTreeAsset.CloneTree(root);

        //STylesheet
        var styleSheet =
            AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/WFC/Scripts/CustomEditors/NodeEditor/WFCNodeEditor.uss");
        root.styleSheets.Add(styleSheet);

        wfcNodeEditorView = root.Q<WFCNodeEditorView>();
        inspectorView = root.Q<InspectorView>();
        wfcNodeEditorView.onNodeSelected = OnNodeSelection;
        OnSelectionChange();
    }

     
    private void OnSelectionChange()
    {
        WFCConfig config = Selection.activeObject as WFCConfig;
        if (config)
        {
            wfcNodeEditorView.PopulateView(config);
        }
    }

    void OnNodeSelection(NodeComponent component)
    {
        inspectorView.UpdateSelection(component);
    }
}