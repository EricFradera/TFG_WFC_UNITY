using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class WFCNodeEditor : EditorWindow
{
    [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("WFC/WFCNodeEditor")]
    public static void OpenWindow()
    {
        WFCNodeEditor wnd = GetWindow<WFCNodeEditor>();
        wnd.titleContent = new GUIContent("WFCNodeEditor");
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
    }
}